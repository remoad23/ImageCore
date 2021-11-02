import { Component, OnInit, ViewChild, Input, AfterViewInit, ElementRef, ViewContainerRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';
import { ImageProcessingService } from '../../services/image-processing.service';

@Component({
  selector: 'layerbox',
  templateUrl: './layerbox.component.html',
  styles: [`
    #layerboxContainer
    {
      width: 100%;
      height: 30px;
      background-color:#272727;
      border: 1px solid black;
      display: flex;
      justify-content: flex-start;
      align-items: center;
    }
    .visibilityContainer
    {
      padding: 10px;
      border-right: 1px solid black;
      display: flex;
      justify-content:center;
      align-items: center;
      width: 10%;
      height: 100%;
      margin-right: 10px
    }
    #layerPreview
    {
      height: 70%;
      width: 35px;
      background-color: #ffffff;
      border: 1px solid black;
      margin-right:10px;
    }
    #maskButton
    {
      height:50%;
      width: 20px;
      background-color: #ffffff;
      border: 1px solid black;
      margin-right:10px;
    }
    #layername
    {
      color: #ffffff;
      font-size: 13px;
    }
    .activeLayer
    {
      background-color: #737373 !important;
    }
  `]
})
export class LayerboxComponent{

  @Input() layerType: boolean;

  @Input() previewImg: any;
  @Input() previewMask: any;

  @Input() layerIndex: number;

  @ViewChild('visibilityCheckbox', { static: false }) visibilityCheckbox: ElementRef;


  constructor(private element: ElementRef, private opencvService: ImageProcessingService) {

  }

  activateLayer() {
    if (this.visibilityCheckbox.nativeElement.checked) {
      this.opencvService.setActiveLayer(this.layerIndex);

      let layerboxes = Array.from(this.element.nativeElement.parentElement.children);
      for (let i = 0; i < layerboxes.length; i++) {
        (<HTMLElement>layerboxes[i]).firstElementChild.classList.remove("activeLayer");
      }

      this.element.nativeElement.firstElementChild.classList.add("activeLayer");
    }
    
  }

  toggleVisibility(event) {
    
    this.opencvService.layerArray[this.layerIndex].toggleLayer(this.visibilityCheckbox.nativeElement.checked);
    event.stopPropagation();
  }

  
}
