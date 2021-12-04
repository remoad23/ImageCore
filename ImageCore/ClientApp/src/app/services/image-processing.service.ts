import { Injectable, Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef, ComponentRef, ComponentFactoryResolver, ReflectiveInjector, HostListener} from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';
import { LayerComponent } from '../components/layer/layer.component';
import { FilterComponent } from '../components/layer/filter.component';
import { ImageviewComponent } from '../layout/imageview/imageview.component';
import { DataTransmitterServiceService } from './data-transmitter-service.service';

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

  private transmitter: DataTransmitterServiceService;


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

  loadFromDB(layers: any) {
    this.ngOpenCVService.isReady$.subscribe(() => { this.loadImageLayers(layers); });
  }

  loadImageLayers(layers: any) {
    
    for (let layer of layers) {
      let newLayerFactory = this.factoryResolver.resolveComponentFactory(LayerComponent);
      let newLayer = this.imgViewComponent.createComponent(newLayerFactory);
      newLayer.instance.loadedFromDB = true;
      newLayer.instance.layerId = layer.imageLayerId;
      newLayer.instance.viewLeft = layer.x;
      newLayer.instance.viewTop = layer.y;
      newLayer.instance.index = layer.z;
      newLayer.instance.isHidden = !layer.visible;
      newLayer.instance.width = layer.width;
      newLayer.instance.height = layer.height;
      newLayer.instance.rotationAngle = layer.rotation;
      newLayer.instance.layerType = layer.layerType;
      newLayer.instance.layerColor = layer.layerColor;
      newLayer.instance.fontSize = layer.fontSize;
      newLayer.instance.fontStrength = layer.fontStrength;
      newLayer.instance.text = layer.text;
      newLayer.instance.filter = layer.filterId;

      newLayer.instance.originalImg = this.parseMat(layer.originalImageMat, layer.width, layer.height);
      newLayer.instance.processedImg = this.parseMat(layer.processedImageMat, layer.width, layer.height);
      newLayer.instance.mask = this.parseMat(layer.maskMat, layer.width, layer.height);

      newLayer.instance.opencvService = this;
      newLayer.instance.componentRef = this.imgViewComponent;
      newLayer.instance.layers = this.layerArray;



    }
  }

  parseMat(matString: string, width: number, height: number) {
    let result = new cv.Mat(height, width, cv.CV_8UC4, new cv.Scalar(0, 0, 0, 255));
    if (matString != "") {
      let mat = JSON.parse(matString);
      let rows = mat.length;
      let cols = mat[0].length;
      let channels = mat[0][0].length;

      for (let i = 0; i < cols; i++) {
        for (let j = 0; j < rows; j++) {
          for (let c = 0; c < channels; c++) {
            result.ucharPtr(j, i)[c] = mat[j][i][c];
          }
        }
      }
    }
    return result;
  }


  setImgViewComponent(reference: any) {
    this.imgViewComponent = reference;
  }

  setTransmitter(reference: any) {
    this.transmitter = reference;
    
  }

  loadProject() {
    this.transmitter.loadProject();
  }

  saveLayer(index: number) {
    this.transmitter.saveLayer(index);
  }

  stringifyMat(mat: any) {
    let valueArray = [];
    for (let i = 0; i < mat.rows; i++) {
      let row = [];
      for (let j = 0; j < mat.cols; j++) {
        let pixelValues = [];
        for (let c = 0; c < 4; c++) {
          pixelValues.push(mat.ucharPtr(i, j)[c]);
        }
        row.push(pixelValues);
      }
      valueArray.push(row);
      
    }

    return JSON.stringify(valueArray);

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
