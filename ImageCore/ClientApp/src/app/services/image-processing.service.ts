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


  // signalR connection
  private connection;


  private projectName = "ImageCore";
  private imgViewComponent;

  public backgroundLayer: LayerComponent;

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

  private undoArray = [];
  private redoArray = [];

  private transmitter: DataTransmitterServiceService;


  constructor(private ngOpenCVService: NgOpenCVService, private componentFactory: ComponentFactoryResolver)
  {
    this.factoryResolver = componentFactory;
    this.layerArray = [];
    this.activeLayer = null;
    this.selectedTool = "move";
  }

  /**
   * adds a new layer instance to the imageview
   * @param event upload event with given image file
   */
  addLayer(event) {
    let newLayerFactory = this.factoryResolver.resolveComponentFactory(LayerComponent);
    let newLayer = this.imgViewComponent.createComponent(newLayerFactory);
    newLayer.instance.setImgSource(event, this.layerArray, this.imgViewComponent, this.layerArray.length, this);
  }

  /**
   * adds a new geometry layer to the imageview
   * @param left
   * @param top
   * @param width
   * @param height
   * @param color
   * @param selectedTool
   */
  addGeometryLayer(left, top, width, height, color, selectedTool) {
    let newLayerFactory = this.factoryResolver.resolveComponentFactory(LayerComponent);
    let newLayer = this.imgViewComponent.createComponent(newLayerFactory);
    newLayer.instance.setLayer(left, top, this.layerArray, this.imgViewComponent, this.layerArray.length, this, selectedTool, [width, height], color);
  }

  /**
   * adds a filter to the active Layer
   * if no layer is active the filter is given to the last layer in the layerArray
   * @param type filtertype
   */
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

  /**
   * checks for the opencv-library and waits until its ready before the layers are loaded from the database
   * @param layers
   */
  loadFromDB(layers: any) {
    this.ngOpenCVService.isReady$.subscribe(() => { this.loadImageLayers(layers); });
  }

  /**
   * loads the image layers from the database response
   * @param layers the response from the database with every saved layer
   */
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

  /**
   * parses a json-string to return an opencv-mat 
   * @param matString the json-string
   * @param width width of the mat
   * @param height height of the mat
   */
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

  /**
   * sets a reference to the imageview
   * @param reference imageview
   */
  setImgViewComponent(reference: any) {
    this.imgViewComponent = reference;
  }

  /**
   * sets a reference to the transmitter service
   * @param reference transmitter service
   */
  setTransmitter(reference: any) {
    this.transmitter = reference;
    
  }

  /**
   * loads the background layer of the project
   * */
  loadBackgroundLayer() {
    let newLayerFactory = this.factoryResolver.resolveComponentFactory(LayerComponent);
    let backgroundLayer = this.imgViewComponent.createComponent(newLayerFactory);
    backgroundLayer.instance.setAsBackground(this.imgViewComponent, this, 500, 500);
  }

  /**
   * sets a reference to the background layer
   * @param layerRef background layer
   */
  setBackgroundLayer(layerRef) {
    this.backgroundLayer = layerRef;
  }

  /**
   * tells the transmitter service to load the saved project from the database after a short timeout to load the opencv library 
   * */
  loadProject() {
    
    setTimeout(() => {
      this.loadBackgroundLayer();
      this.transmitter.loadProject();
    },600);
    
  }

  /**
   * Downloads the imagelayers by combining every layer into a single canvas
   * */
  exportProject() {
    let backgroundMat = new cv.Mat();
    this.backgroundLayer.getMats()[1].copyTo(backgroundMat);

    let backLeft = this.backgroundLayer.viewLeft;
    let backTop = this.backgroundLayer.viewTop;

    let layerMat = new cv.Mat();
    let layerLeft = 0;
    let layerTop = 0;
    let xDiff = 0;
    let yDiff = 0;
    for (let l = 0; l < this.layerArray.length; l++) {
      if (this.layerArray[l].imageLayer) {
        this.layerArray[l].getMats()[1].copyTo(layerMat);
        layerLeft = Math.round(this.layerArray[l].viewLeft);
        layerTop = Math.round(this.layerArray[l].viewTop);
        xDiff = Math.round(layerLeft - backLeft);
        yDiff = Math.round(layerTop - backTop);

        let startX = Math.round(Math.max(0, layerLeft - backLeft));
        let endX = Math.round(Math.min(this.backgroundLayer.width, (layerLeft + this.layerArray[l].width) - backLeft));
        let startY = Math.round(Math.max(0, layerTop - backTop));
        let endY = Math.round(Math.min(this.backgroundLayer.height, (layerTop + this.layerArray[l].height) - backTop));

        let alphavalue = 0;
        for (let i = startX; i < endX; i++) {
          for (let j = startY; j < endY; j++) {
            if (i < this.backgroundLayer.width)
              for (let c = 0; c < 3; c++) {
                // normalize for better calculations
                alphavalue = layerMat.ucharPtr(j - yDiff, i - xDiff)[3] / 255;
                let normedBack = backgroundMat.ucharPtr(j, i)[c] / 255;
                let normedLayer = layerMat.ucharPtr(j - yDiff, i - xDiff)[c] / 255;
                // linear blend operation
                backgroundMat.ucharPtr(j, i)[c] = Math.floor((alphavalue * normedLayer + (1 - alphavalue) * normedBack) * 255);
              }
          }
        }
      }
      
    }
    cv.imshow(this.backgroundLayer.originalImgView.nativeElement.id, backgroundMat);
    let link = this.backgroundLayer.originalImgView.nativeElement.toDataURL('image/png');
    console.log(link);
    link = link.replace(/^data:image\/[^;]*/, 'data:application/octet-stream');

    let exportButton = <HTMLAnchorElement>document.getElementById("exportButton");
    exportButton.href = link;
    exportButton.download = this.projectName +".png";

    setTimeout(() => {
      exportButton.click();
    }, 100);

    backgroundMat.delete();
    layerMat.delete();
  }

  /**
   * Tells the transmitter service to save the indexed layer
   * @param index layer index
   */
  saveLayer(index: number) {
    this.transmitter.saveLayer(index);
  }

  /**
   * converts the values of an opencv-mat object into a json-string
   * @param mat
   */
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

  /**
   * updates the indices of the layers
   * */
  updateLayerArray() {
    for (let i = 0; i < this.layerArray.length; i++) {
      if (this.layerArray[i].imageLayer) {
        this.layerArray[i].updateZIndex(i);
      }
    }
  }

  /**
   * tells the layer to create a mask with the maskPreview object
   * @param layerIdx index of the layer that will be masked
   */
  createMask(layerIdx) {
    this.layerArray[layerIdx].createMask(this.maskPreview);
  }

  /**
   * toggles the visibility of the mask buttons. they are only active if
   * a selection was made with the mask-tool
   * @param enabled
   * @param maskPreview
   */
  toggleMaskUI(enabled, maskPreview) {
    this.masksEnabled = enabled;
    this.maskPreview = maskPreview;
  }

  /**
   * sets the active layer of the project and deactivates the prior active layer
   * @param idx index of the new active layer
   */
  setActiveLayer(idx: number) {
    if (this.activeLayer != null && this.layerArray[this.activeLayer].imageLayer) {
      this.layerArray[this.activeLayer].deactivateTransformBox();
    }
    this.activeLayer = idx;
    if (this.selectedTool == 'move' && this.layerArray[this.activeLayer].imageLayer) {
      this.layerArray[this.activeLayer].activateTransformBox();
    }

  }

  /**
   * deletes the active layer from the layerArray
   * */
  deleteActiveLayer() {
    if (this.activeLayer !== null) {
      this.layerArray[this.activeLayer].deleteLayer();
      this.layerArray.splice(this.activeLayer, 1);
      
      this.activeLayer = null;
    }
  }

  /**
   * deletes the indexed layer from the layerArray
   * */
  deleteLayer(index) {
    if (this.layerArray[index] != null) {
      this.layerArray[index].deleteLayer();
      this.layerArray.splice(index, 1);

      this.activeLayer = null;
    }
  }

  /**
   * moves the selected layer by the given values
   * @param diffX movement in x direction
   * @param diffY movement in y direction
   */
  moveSelectedLayer(diffX, diffY) {
    if (this.activeLayer !== null) {
      this.layerArray[this.activeLayer].viewLeft += diffX;
      this.layerArray[this.activeLayer].viewTop += diffY;
    }
  }

  // sends a string to the signalr connection
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

  /**
   * adds a change to the undoArray that can be undone
   * @param command change command
   * @param index layer index
   */
  addUndo(command, index) {
    if (this.undoArray.length >= 10) {
      this.undoArray.shift();
    }
    let undolayer = Object.assign({}, this.layerArray[index]);
    undolayer.processedImg = new cv.Mat();
    this.layerArray[index].processedImg.copyTo(undolayer.processedImg);
    let undo = { command: command, index: index, layer: undolayer } ;
    this.undoArray.push(undo);
  }

  /**
   * undoes the latest change in the undoArray
   * */
  undo() {
    console.log(this.undoArray.length);
    if (this.undoArray.length > 0) {
      let undo = this.undoArray.pop();
      if (undo.command == "changeLayer") {
        this.layerArray[undo.index].updateLayer(undo.layer);
      }
      else if (undo.command == "addLayer") {
        this.deleteLayer(undo.index);
      }
      else if (undo.command == "deleteLayer") {
        let newLayerFactory = this.factoryResolver.resolveComponentFactory(LayerComponent);
        let newLayer = this.imgViewComponent.createComponent(newLayerFactory);
        newLayer.instance.setImgSource(undo.layer, this.layerArray, this.imgViewComponent, undo.index, this);
      }
      this.addRedo(undo);
    }
    
  }

  /**
   * adds an undone change to the redoArray
   * @param undo undone change
   */
  addRedo(undo) {
    if (this.redoArray.length >= 10) {
      this.redoArray.shift();
    }
    this.redoArray.push(undo);
  }

  /**
   * redoes the latest undone change in the redoArray
   * */
  redo() {
    console.log(this.redoArray.length);
    if (this.redoArray.length > 0) {
      let redo = this.redoArray.pop();
      if (redo.command == "changeLayer") {
        this.layerArray[redo.index].updateLayer(redo.layer);
      }
      else if (redo.command == "addLayer") {
        let newLayerFactory = this.factoryResolver.resolveComponentFactory(LayerComponent);
        let newLayer = this.imgViewComponent.createComponent(newLayerFactory);
        newLayer.instance.setImgSource(redo.layer, this.layerArray, this.imgViewComponent, redo.index, this);
      }
      else if (redo.command == "deleteLayer") {
        this.deleteLayer(redo.index);
      }
      this.undoArray.push(redo);
    }
  }

  /**
   * eventhandler that deletes all layers to free RAM-space before the website is closed
   * @param event
   */
  @HostListener("window:beforeunload", ["$event"]) unloadHandler(event: Event) {
    console.log("Processing beforeunload...");
    for (let i = 0; i < this.layerArray.length; i++) {
      this.layerArray[i].deleteLayer();
    }
    event.returnValue = false;
  }

  /**
   * turns a hex-color into the rgb-colorspace
   * @param hex
   */
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
