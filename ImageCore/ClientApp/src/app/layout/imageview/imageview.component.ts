import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef, HostListener } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';
import { ImageProcessingService } from '../../services/image-processing.service';

@Component({
  selector: 'imageview',
  templateUrl: './imageview.component.html',
  styles: [`
    :host
    {
      position: relative;
      width: 81vw;
      height: 95vh;
      background-color:#272727;
      overflow: scroll;
    }
    .dragged
    {
      cursor: grab;
    }
    .layerDragPreview{
      position: absolute;
      border: 2px dashed #dbdbdb;
      z-index: 99;
    }
  `]
})
export class ImageviewComponent{
  @ViewChild('dragPreview', { static: false }) dragPreview: ElementRef;
  @ViewChild('imageviewContainer', { read: ViewContainerRef, static: false }) imageviewContainer: ViewContainerRef;

  private draggingView: boolean;
  private dragSpeed = 0.07;
  private dragX: number;
  private dragY: number;

  public viewScale = 1;
  private minScale = 0.1;
  private maxScale = 4;
  private ctrlPressed = false;
  private scrollFactor = 1.1;

  public movingLayer: boolean;
  public creatingGeometry = false;
  private previewWidth = 0;
  private previewHeight = 0;
  private previewDisplay = "none";

  private offsetLeft = 0;
  private offsetTop = 0;

  constructor(private element: ElementRef, private opencvService: ImageProcessingService) {
    this.draggingView = false;
  }

  ngAfterViewInit() {
    this.opencvService.setImgViewComponent(this.imageviewContainer);
  }

  @HostListener('mousedown', ['$event']) onMouseDown($event) {
    if ($event.button === 0) {
      if (this.opencvService.selectedTool == "move") {
        this.movingLayer = true;
        this.dragX = $event.clientX;
        this.dragY = $event.clientY;
      }
      else if (this.opencvService.selectedTool == "rectangle" || this.opencvService.selectedTool == "text" || this.opencvService.selectedTool == "selectRect") {
        this.previewWidth = 0;
        this.previewHeight = 0;
        this.creatingGeometry = true;
        this.offsetLeft = document.getElementById('toolbarContainer').offsetWidth;
        this.offsetTop = document.getElementById('menubarContainer').offsetHeight;
        this.dragX = $event.clientX - this.offsetLeft;
        this.dragY = $event.clientY - this.offsetTop;
        this.previewDisplay = "block";
        
      }
    }
    if ($event.button === 1) {
      this.draggingView = true;
      this.dragX = $event.clientX;
      this.dragY = $event.clientY;
      event.stopPropagation();
      event.preventDefault();
      document.body.style.cursor = "grabbing";
    } 
  }

  @HostListener('mouseup', ['$event']) onMouseUp($event) {
    if ($event.button === 0) {
      if (this.opencvService.selectedTool == "move") {
        this.movingLayer = false;
        if (this.opencvService.activeLayer != null && this.opencvService.layerArray[this.opencvService.activeLayer].dragTransform) {
          this.opencvService.layerArray[this.opencvService.activeLayer].endTransform();
        }
        else if (this.opencvService.activeLayer != null && this.opencvService.layerArray[this.opencvService.activeLayer].dragRotation) {
          this.opencvService.layerArray[this.opencvService.activeLayer].endRotation();
        }
      }
      else if (this.opencvService.selectedTool == "rectangle" || this.opencvService.selectedTool == "text") {
        this.creatingGeometry = false;
        if (this.previewWidth > 1 && this.previewHeight > 1) {
          this.opencvService.addGeometryLayer(this.dragX, this.dragY, this.previewWidth, this.previewHeight);
        }
        this.previewDisplay = "none";
      }
      else if (this.opencvService.selectedTool == "selectRect") {
        this.creatingGeometry = false;
        if (this.previewWidth > 1 && this.previewHeight > 1) {
          let previewRect = this.dragPreview.nativeElement.getBoundingClientRect();
          this.opencvService.toggleMaskUI(true, previewRect);
        }
        else {
          this.opencvService.toggleMaskUI(false, null);
          this.previewDisplay = "none";
        }
      }
    }
    if ($event.button === 1) {
      this.draggingView = false;
      document.body.style.cursor = "default";
      event.stopPropagation();
      event.preventDefault();
    }
  }

  @HostListener('mousemove', ['$event']) mouseMoveEvent($event) {
    if (this.movingLayer) {
      let differenceX = $event.clientX - this.dragX;
      let differenceY = $event.clientY - this.dragY;
      if (this.opencvService.activeLayer != null && this.opencvService.layerArray[this.opencvService.activeLayer].dragTransform) {
        this.opencvService.layerArray[this.opencvService.activeLayer].transformImage(differenceX, differenceY);
      }
      else if (this.opencvService.activeLayer != null && this.opencvService.layerArray[this.opencvService.activeLayer].dragRotation) {
        this.opencvService.layerArray[this.opencvService.activeLayer].rotateImage($event.clientX, $event.clientY);
      }
      else {
        this.opencvService.moveSelectedLayer(differenceX / this.viewScale, differenceY / this.viewScale);
      }
      this.dragX = $event.clientX;
      this.dragY = $event.clientY;
    }
    else if (this.creatingGeometry) {
      this.previewWidth = $event.clientX - this.offsetLeft - this.dragX;
      this.previewHeight = $event.clientY - this.offsetTop - this.dragY;
    }
    else if (this.draggingView) {
      let differenceX = $event.clientX - this.dragX;
      let differenceY = $event.clientY - this.dragY;
      this.element.nativeElement.scrollLeft -= differenceX;
      this.element.nativeElement.scrollTop -= differenceY;
      this.dragX = $event.clientX;
      this.dragY = $event.clientY;
    }
  }

  @HostListener('wheel', ['$event']) onMousewheel($event) {
    if (this.ctrlPressed) {
      $event.preventDefault();
      if ($event.deltaY > 0) {
        this.viewScale *= this.scrollFactor;
        if (this.viewScale > this.maxScale) {
          this.viewScale = this.maxScale;
        }
        else {
          this.scaleLayerViews();
        }
      }
      if ($event.deltaY < 0) {
        this.viewScale /= this.scrollFactor;
        if (this.viewScale < this.minScale) {
          this.viewScale = this.minScale;
        }
        else {
          this.scaleLayerViews();
        }
        
      }
    }
    //$event.srcElement.style.setProperty('transition', 'all 200ms ease-out')
    //$event.srcElement.style.setProperty('transform', "scale(" + 1.25 + ")")
  }

  scaleLayerViews() {
    for (let i = 0; i < this.opencvService.layerArray.length; i++) {
      this.opencvService.layerArray[i].scaleView(this.viewScale);
    }
  }

  @HostListener('document:keydown.control', ['$event']) keydownEvent($event) {
    this.ctrlPressed = true;
  }

  @HostListener('document:keyup.control', ['$event']) keyupEvent($event) {
    this.ctrlPressed = false;
  }




  
}
