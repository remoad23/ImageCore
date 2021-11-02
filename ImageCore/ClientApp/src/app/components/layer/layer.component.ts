import { Component, OnInit, ViewChild, AfterViewInit, HostListener, ElementRef, ViewContainerRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';
import { ImageProcessingService } from '../../services/image-processing.service';
import { FilterComponent } from '../../components/layer/filter.component';
import { DataTransmitterServiceService } from "../../services/data-transmitter-service.service";

@Component({
  selector: 'layer',
  templateUrl: './layer.component.html',
  styles: [`
    :host{
      position: relative;
    }
    .layerCanvas{
      position: relative;
    }
    .rotatebox
    {
      position: relative;
      border: 1px dashed rgba(219, 219, 219, 0);
    }
    .transformbox
    {
      position: absolute;
      width: fit-content;
      height: fit-content;
     
      
    }
    .grabbox{
      background-color: #ffffff;
      position: absolute;
    }
    .rotationHandle
    {
      border-radius: 100%;
      background-color: #ffffff;
      position: absolute;
    }
    .layerviewContainer{
    }
  `]
})
export class LayerComponent{

  @ViewChild('rotatebox', { static: false }) rotatebox: ElementRef;
  @ViewChild('transformbox', { static: false }) transformbox: ElementRef;
  @ViewChild('layerviewContainer', { static: false }) layerviewContainer: ElementRef;
  @ViewChild('layerView', { static: false }) layerView: ElementRef;
  @ViewChild('originalImgView', { static: false }) originalImgView: ElementRef;
  @ViewChild('maskImgView', { static: false }) maskImgView: ElementRef;

  //css
  public startPadding = 20;
  public padding = 20;
  public startGrabboxSize = 10;
  public grabboxSize = 10;

  //Image position
  public viewLeft = 0;
  public viewTop = 0;

  public width;
  public height;

  public imageLeft;
  public imageTop;

  public imageCenterX;
  public imageCenterY;

  public viewCenterX;
  public viewCenterY;

  private display = "none";
  public isHidden = false;

  private previewScale = 1;
  public scaleValue = 1;
  private rotationAngle = 0;

  private componentRef: any;
  private index: number;

  private originalImg: any;
  private processedImg: any;
  private mask: any;

  private masked = false;

  private imgSource;

  private layers: any;

  public canvasURL: any;
  public maskURL: any;

  public dragRotation = false;
  public dragTransform = false;
  private startDragX;
  private startDragY;

  private dragBoxDirectionX;
  private dragBoxDirectionY;

  private opencvService: ImageProcessingService;

  public layerType = "image";
  private layerSize = [0, 0];
  public layerColor = "#ffffff";

  //text
  public fontSize = 2;
  public fontStrength = 2;
  public text = "Placeholder";

  //Filter
  public filter: FilterComponent;
  public imageLayer = true;


  constructor(private ngOpenCVService: NgOpenCVService,) {

    this.canvasURL == "";
    this.maskURL == "";
    this.filter = null;
    
  }

  ngAfterViewInit() {
    if (this.layerType === "rectangle" || this.layerType === "text") {
      this.loadEmptyCanvas();
    }
    else {
      this.loadImage();
    }
  }

  loadImage() {
    if (this.imgSource.target.files.length) {
      const reader = new FileReader();
      const load$ = fromEvent(reader, 'load');
      load$
        .pipe(
          switchMap(() => {
            return this.ngOpenCVService.loadImageToHTMLCanvas(`${reader.result}`, this.originalImgView.nativeElement);
          })
        )
        .subscribe(
          () => {
             this.loadImageToCanvas();
          },
          err => {
            console.log('Error loading image', err);
          }
      );
      reader.readAsDataURL(this.imgSource.target.files[0]);
    }
  }

  loadImageToCanvas() {
    this.originalImgView.nativeElement.id = "originalImgView" + this.layers.length;
    this.layerView.nativeElement.id = "layerView" + this.layers.length;
    this.originalImg = cv.imread(this.originalImgView.nativeElement.id);
    this.processedImg = new cv.Mat();
    this.originalImg.copyTo(this.processedImg);
    this.mask = new cv.Mat(this.originalImg.rows, this.originalImg.cols, this.originalImg.type(), new cv.Scalar(0, 0, 0, 255));
    cv.imshow(this.layerView.nativeElement.id, this.processedImg);
    this.canvasURL = this.layerView.nativeElement.toDataURL();
    this.centerImage();
    this.width = this.processedImg.cols;
    this.height = this.processedImg.rows;
    this.layers.push(this);
    this.updateZIndex(this.index);
  }

  loadEmptyCanvas() {
    this.originalImgView.nativeElement.id = "originalImgView" + this.layers.length;
    this.layerView.nativeElement.id = "layerView" + this.layers.length;
    this.originalImg = new cv.Mat(this.layerSize[1], this.layerSize[0], cv.CV_8UC4, new cv.Scalar(0, 0, 0, 0));
    this.processedImg = new cv.Mat();
    this.originalImg.copyTo(this.processedImg);
    this.mask = new cv.Mat(this.originalImg.rows, this.originalImg.cols, this.originalImg.type(), new cv.Scalar(0, 0, 0, 255));
    cv.imshow(this.layerView.nativeElement.id, this.processedImg);
    this.canvasURL = this.layerView.nativeElement.toDataURL();
    this.width = this.processedImg.cols;
    this.height = this.processedImg.rows;
    this.layers.push(this);
    this.updateZIndex(this.index);
    this.scaleView(this.scaleValue);

    this.addGeometry();
  }

  scaleImage(scaleX: number, scaleY: number) {
    cv.resize(this.processedImg, this.processedImg, new cv.Size(scaleX, scaleY), 0, 0, cv.INTER_AREA);
    cv.resize(this.mask, this.mask, new cv.Size(scaleX, scaleY), 0, 0, cv.INTER_AREA);
    this.width = this.processedImg.cols;
    this.height = this.processedImg.rows;
    if (this.masked) {
      this.applyMask();
    }
    else {
      cv.imshow(this.layerView.nativeElement.id, this.processedImg);
    }
    

    this.layerView.nativeElement.style.width = (this.width) + "px";
    this.layerView.nativeElement.style.height = (this.height) + "px";

    this.transformbox.nativeElement.style.width = (this.width + this.padding * 2) + "px";
    this.transformbox.nativeElement.style.height = (this.height + this.padding * 2) + "px";

  }

  setLayer(left, top, layerArray, cmp, i, service, layerType, layerSize, color) {
    this.imageLeft = left;
    this.imageTop = top;
    console.log(left, top);
    //this.viewLeft = 
    this.layers = layerArray;
    this.componentRef = cmp;
    this.scaleValue = this.componentRef._view.component.viewScale;
    this.index = i;
    this.opencvService = service;
    this.layerType = layerType;
    this.layerSize[0] = layerSize[0] / this.scaleValue;
    this.layerSize[1] = layerSize[1] / this.scaleValue;
    this.layerColor = color;
    this.placeImage();
  }

  addGeometry() {
    if (this.layerType === "rectangle") {
      this.addRectangle();
    }
    else if (this.layerType === "text") {
      this.addText();
    }
    this.canvasURL = this.layerView.nativeElement.toDataURL();
  }

  updateGeometry() {
    this.clearGeometry();
    this.addGeometry();
  }

  clearGeometry() {
    this.originalImg.copyTo(this.processedImg);
  }

  addRectangle() {
    let rgb = this.opencvService.hexToRgb(this.layerColor);
    cv.rectangle(this.processedImg, new cv.Point(0, 0), new cv.Point(this.width, this.height), new cv.Scalar(rgb[0], rgb[1], rgb[2], 255), -1);
    cv.imshow(this.layerView.nativeElement.id, this.processedImg);
  }

  addText() {
    let font = cv.FONT_HERSHEY_SIMPLEX;
    let rgb = this.opencvService.hexToRgb(this.layerColor);
    cv.putText(this.processedImg, this.text, new cv.Point(0, this.height / 2), font, this.fontSize, new cv.Scalar(rgb[0], rgb[1], rgb[2], 255), this.fontStrength, cv.LINE_AA);
    cv.imshow(this.layerView.nativeElement.id, this.processedImg);
  }

  setImgSource(event, layerArray,cmp,i,service) {
    this.imgSource = event;
    this.layers = layerArray;
    this.componentRef = cmp;
    this.index = i;
    this.opencvService = service;
  }

  updateZIndex(i: number) {
    this.transformbox.nativeElement.parentElement.style.zIndex = i;
  }

  scaleView(scale) {
    this.scaleValue = scale;
    this.transformbox.nativeElement.style.setProperty('transform', "scale(" + scale + ")");
    //this.padding = this.startPadding / (this.scaleValue * 0.8);
    //this.grabboxSize = this.startGrabboxSize / (this.scaleValue * 0.8);
    this.transformbox.nativeElement.style.width = (this.width + this.padding * 2) + "px";
    this.transformbox.nativeElement.style.height = (this.height + this.padding * 2) + "px";

  }

  determineStartScale(width, height) {
    let widthRatio = width / this.transformbox.nativeElement.offsetWidth;
    let heightRatio = height / this.transformbox.nativeElement.offsetHeight;

    if (widthRatio <= heightRatio) {
      this.scaleValue = widthRatio - 0.2;
    }
    else {
      this.scaleValue = heightRatio - 0.2;
    }
    this.scaleView(this.scaleValue);
    this.componentRef._view.component.viewScale = this.scaleValue;
  }

  centerImage() {
    let imageView = document.getElementById("imageviewContainer").parentElement;

    this.viewCenterX = imageView.offsetLeft + imageView.offsetWidth / 2;
    this.viewCenterY = imageView.offsetTop + imageView.offsetHeight / 2;
    this.viewLeft = this.viewCenterX - this.layerView.nativeElement.offsetWidth / 2 - imageView.offsetLeft;
    this.viewTop = this.viewCenterY - this.layerView.nativeElement.offsetHeight / 2 - imageView.offsetTop;  
    if (this.layers.length == 0) {
      this.determineStartScale(imageView.offsetWidth, imageView.offsetHeight);
    }
    else {
      this.scaleView(this.componentRef._view.component.viewScale);
    }
    
  }

  placeImage() {
    let imageView = document.getElementById("imageviewContainer").parentElement;
    this.viewCenterX = imageView.offsetLeft + imageView.offsetWidth / 2;
    this.viewCenterY = imageView.offsetTop + imageView.offsetHeight / 2;
    console.log(this.imageLeft, this.viewCenterX);
    this.viewLeft = this.viewCenterX - (this.viewCenterX - this.imageLeft) / this.scaleValue - this.padding;
    this.viewTop = this.viewCenterY - (this.viewCenterY - this.imageTop) / this.scaleValue  - this.padding / 2;
  }

  activateTransformBox() {
    this.display = "block";
    this.rotatebox.nativeElement.style.border = "1px dashed rgba(219, 219, 219,1)";
  }

  deactivateTransformBox() {
    this.display = "none";
    this.rotatebox.nativeElement.style.border = "1px dashed rgba(219, 219, 219, 0)";
  }

  startTransformDrag(event, directionX, directionY) {
    if (event.button === 0) {
      event.preventDefault();
      this.dragTransform = true;
      this.dragBoxDirectionX = directionX;
      this.dragBoxDirectionY = directionY;
    }
  }

  startRotation(event) {
    if (event.button === 0) {
      event.preventDefault();
      this.dragRotation = true;
      this.startDragX = event.clientX;
      this.startDragY = event.clientY;
      this.imageCenterX = this.transformbox.nativeElement.getBoundingClientRect().left + this.transformbox.nativeElement.getBoundingClientRect().width / 2;
      this.imageCenterY = this.transformbox.nativeElement.getBoundingClientRect().top + this.transformbox.nativeElement.getBoundingClientRect().height / 2;
      console.log(this.transformbox.nativeElement.getBoundingClientRect());
    }
  }

  rotateImage(positionX, positionY) {
    let rotationVector = [positionX - this.imageCenterX, positionY - this.imageCenterY];
    let startVector = [0, -1];
    let angle = Math.acos((rotationVector[0] * startVector[0] + rotationVector[1] * startVector[1]) /
      (Math.sqrt(Math.pow(rotationVector[0], 2) + Math.pow(rotationVector[1], 2)) * Math.sqrt(Math.pow(startVector[0], 2) + Math.pow(startVector[1], 2)))) * (180 / Math.PI);
    

    if (this.imageCenterX > positionX) {
      angle *= -1;
    }
    this.rotationAngle = angle;


    this.rotatebox.nativeElement.style.setProperty('transform', "rotate(" + angle + "deg)");
    

  }

  endRotation() {
    this.dragRotation = false;

    /*let rows = this.processedImg.cols;
    let cols = this.processedImg.rows;
    let center = new cv.Point(rows / 2, cols / 2);
    let rotationMatrix = cv.getRotationMatrix2D(center, this.rotationAngle, 1);

    let cos = Math.abs(rotationMatrix.ucharAt(0, 0));
    let sin = Math.abs(rotationMatrix.ucharAt(0, 1));


    let newBoundWidth = cols * sin + rows * cos;
    let newBoundHeight = cols * cos + rows * sin;

    rotationMatrix.ucharPtr(0, 2)[0] += newBoundWidth / 2 - center[0];
    rotationMatrix.ucharPtr(1, 2)[0] += newBoundHeight / 2 - center[1];


    cv.warpAffine(this.processedImg, this.processedImg, rotationMatrix, new cv.Size(newBoundWidth, newBoundHeight), cv.INTER_AREA, cv.BORDER_CONSTANT, new cv.Scalar());
    this.layerView.nativeElement.style.width = newBoundWidth+"px";
    this.layerView.nativeElement.style.height = newBoundHeight + "px";
    cv.imshow(this.layerView.nativeElement.id, this.processedImg);*/
  }

  transformImage(x, y) {
    if (this.dragBoxDirectionX == -1) {
      this.transformbox.nativeElement.style.left = (this.transformbox.nativeElement.offsetLeft + x) + "px";
    }
    if (this.dragBoxDirectionY == -1) {
      this.transformbox.nativeElement.style.top = (this.transformbox.nativeElement.offsetTop + y) + "px";
    }
    this.transformbox.nativeElement.style.width = (this.transformbox.nativeElement.offsetWidth + (x * this.dragBoxDirectionX) / this.scaleValue) + "px";
    this.transformbox.nativeElement.style.height = (this.transformbox.nativeElement.offsetHeight + (y * this.dragBoxDirectionY) / this.scaleValue) + "px";

    this.layerView.nativeElement.style.width = (this.transformbox.nativeElement.offsetWidth - this.padding * 2)+"px";
    this.layerView.nativeElement.style.height = (this.transformbox.nativeElement.offsetHeight - this.padding * 2)+"px";
  }

  endTransform() {
    this.dragTransform = false;
    this.scaleImage(this.transformbox.nativeElement.offsetWidth - this.padding * 2, this.transformbox.nativeElement.offsetHeight - this.padding * 2);
  }

  toggleLayer(show: boolean) {
    if (show) {
      this.isHidden = false;

    }
    else {
      this.isHidden = true;
    }
  }

  createMask(maskPreview) {
    var boundingRect = this.layerView.nativeElement.getBoundingClientRect();
    if ((maskPreview.right > boundingRect.left && maskPreview.left < boundingRect.right) && (maskPreview.bottom > boundingRect.top && maskPreview.top < boundingRect.bottom)) {
      let scale = this.width / boundingRect.width;
      let startX = Math.max(0, (maskPreview.left - boundingRect.left) * scale);
      let endX = Math.min(this.width, (maskPreview.right - boundingRect.left) * scale);
      let startY = Math.max(0, (maskPreview.top - boundingRect.top) * scale);
      let endY = Math.min(this.height, (maskPreview.bottom - boundingRect.top) * scale);
      this.masked = true;
      for (let i = startX; i < endX; i++) {
        for (let j = startY; j < endY; j++) {
          for (let c = 0; c < 3; c++) {
            this.mask.ucharPtr(j, i)[c] = 255;
          }
        }
      }
      cv.imshow(this.maskImgView.nativeElement, this.mask);
      this.maskURL = this.maskImgView.nativeElement.toDataURL();
      this.applyMask();
    }
    
  }

  applyMask() {
    if (this.masked) {
      let maskedImg = new cv.Mat();
      if (this.filter != null) {
        this.filter.filteredImg.copyTo(maskedImg);
      }
      else {
        this.processedImg.copyTo(maskedImg);
      }
      for (let i = 0; i < this.width; i++) {
        for (let j = 0; j < this.height; j++) {
          if (this.mask.ucharPtr(j, i)[0] != 255) {
            maskedImg.ucharPtr(j, i)[3] = 0;
          }
        }
      }
      cv.imshow(this.layerView.nativeElement.id, maskedImg);
      maskedImg.delete();
    }
  }

  applyFilter() {
    if (this.filter != null) {
      this.filter.apply(this.processedImg, this.layerView.nativeElement.id, this);
      
      this.applyMask();
    }
  }

  deleteLayer() {
    this.originalImg.delete();
    this.mask.delete();
    this.processedImg.delete();
    this.componentRef.remove(this.index);
  }

  getMats() {
    return [this.originalImg, this.processedImg, this.mask];
  }


  
}
