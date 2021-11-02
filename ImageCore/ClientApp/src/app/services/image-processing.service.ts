import { Injectable, Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef, ComponentRef, ComponentFactoryResolver, ReflectiveInjector, HostListener} from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';
import { LayerComponent } from '../components/layer/layer.component';
import { FilterComponent } from '../components/layer/filter.component';
import { ImageviewComponent } from '../layout/imageview/imageview.component';

@Injectable({
  providedIn: 'root'
})
/**
 * Class handles the image processing methods
 */
export class ImageProcessingService {

  // data from backend will be put in here
  private receivedData: any;

  // signalR connection
  private connection;

  private imgViewComponent;

  public layerArray: any;
  public activeLayer: number;

  private factoryResolver: any;

  public selectedTool: string;

  //colors
  public toolColor = ["#ffffff", "#000000"];
  public activeColor = 0;

  //masks
  public masksEnabled = false;
  private maskPreview = null;


  constructor(private ngOpenCVService: NgOpenCVService, private componentFactory: ComponentFactoryResolver)
  {
    this.factoryResolver = componentFactory;
    this.layerArray = [];
    this.activeLayer = null;
    this.selectedTool = "move";
  }


  addLayer(event) {
    let newLayerFactory = this.factoryResolver.resolveComponentFactory(LayerComponent);
    let newLayer = this.imgViewComponent.createComponent(newLayerFactory);
    newLayer.instance.setImgSource(event, this.layerArray, this.imgViewComponent, this.layerArray.length, this);
  }

  addGeometryLayer(left, top, width, height, color, selectedTool) {
    let newLayerFactory = this.factoryResolver.resolveComponentFactory(LayerComponent);
    let newLayer = this.imgViewComponent.createComponent(newLayerFactory);
    newLayer.instance.setLayer(left, top, this.layerArray, this.imgViewComponent, this.layerArray.length, this, selectedTool, [width, height], color);
  }

  addFilter(type: string) {
    let newFilterFactory = this.factoryResolver.resolveComponentFactory(FilterComponent);
    let newFilter = this.imgViewComponent.createComponent(newFilterFactory);
    newFilter.instance.setFilter(type, this.imgViewComponent, this.layerArray.length, this);
    if (this.activeLayer != null && this.activeLayer != this.layerArray.length - 1) {
      this.layerArray.splice(this.activeLayer + 1, 0, newFilter.instance);
      newFilter.instance.setFilter(type, this.imgViewComponent, this.activeLayer + 1, this);
      this.layerArray[this.activeLayer].filter = newFilter.instance;
    }
    else {
      this.layerArray.push(newFilter.instance);
      newFilter.instance.setFilter(type, this.imgViewComponent, this.layerArray.length - 1, this);
      this.layerArray[this.layerArray.length-2].filter = newFilter.instance;
    }
  }


  setImgViewComponent(reference: any) {
    this.imgViewComponent = reference;
  }

  updateLayerArray() {
    for (let i = 0; i < this.layerArray.length; i++) {
      if (this.layerArray[i].imageLayer) {
        this.layerArray[i].updateZIndex(i);
      }
    }
  }

  createMask(layerIdx) {
    this.layerArray[layerIdx].createMask(this.maskPreview);
  }

  toggleMaskUI(enabled, maskPreview) {
    this.masksEnabled = enabled;
    this.maskPreview = maskPreview;
  }

  setActiveLayer(idx: number) {
    if (this.activeLayer != null && this.layerArray[this.activeLayer].imageLayer) {
      this.layerArray[this.activeLayer].deactivateTransformBox();
    }
    this.activeLayer = idx;
    if (this.selectedTool == 'move' && this.layerArray[this.activeLayer].imageLayer) {
      this.layerArray[this.activeLayer].activateTransformBox();
    }

  }

  deleteActiveLayer() {
    if (this.activeLayer !== null) {
      this.layerArray[this.activeLayer].deleteLayer();
      this.layerArray.splice(this.activeLayer, 1);
      
      this.activeLayer = null;
    }
  }

  moveSelectedLayer(diffX, diffY) {
    if (this.activeLayer !== null) {
      this.layerArray[this.activeLayer].viewLeft += diffX;
      this.layerArray[this.activeLayer].viewTop += diffY;
    }
  }

  updateData(text:string)
  {
    this.connection.send("Send",text);
  }

  reconnect()
  {

  }

  startConnection()
  {
    this.connection.start();
  }

  abortConnection()
  {
    this.connection.stop();
  }

  private initEvenets()
  {

  }

  @HostListener("window:beforeunload", ["$event"]) unloadHandler(event: Event) {
    console.log("Processing beforeunload...");
    for (let i = 0; i < this.layerArray.length; i++) {
      this.layerArray[i].deleteLayer();
    }
    event.returnValue = false;
  }


  hexToRgb(hex) {
    var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
    if (result) {
      return [parseInt(result[1], 16),
      parseInt(result[2], 16),
      parseInt(result[3], 16)];
    }
    else {
      return null;
    }
  }
}
