import { Component, OnInit, ViewChild, AfterViewInit, HostListener, ElementRef, ViewContainerRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';
import { ImageProcessingService } from '../../services/image-processing.service';
import { LayerComponent } from '../../components/layer/layer.component';

@Component({
  selector: 'filter',
  templateUrl: './filter.component.html',
  styles: [`
    :host{
    }
  `]
})
export class FilterComponent{

  private componentRef: any;
  private index: number;
  private opencvService: any;

  public filterType: string;

  private brightness: number;
  private contrast: number;

  private hue: number;
  private saturation: number;
  private value: number;

  public imageLayer = false;

  public filteredImg: any;



  constructor(private ngOpenCVService: NgOpenCVService) {
    this.brightness = 0;
    this.contrast = 1;
    this.hue = 0;
    this.saturation = 1;
    this.value = 1;
    this.filteredImg = new cv.Mat();
    
  }

  setFilter(filterType, cmp, i, service) {
    this.filterType = filterType;
    this.componentRef = cmp;
    this.index = i;
    this.opencvService = service;

  }

  apply(image, id, mask) {
    if (this.filterType == "brightness_contrast") {
      this.applyBrightnessContrast(image, id);
    }
    else if (this.filterType == "hsv_change") {
      this.applyHSVChange(image, id, mask);
    }
  }

  applyBrightnessContrast(image, id) {
    image.copyTo(this.filteredImg);
    
    for (let i = 0; i < image.cols; i++) {
      for (let j = 0; j < image.rows; j++) {
        for (let c = 0; c < 3; c++) {
          
          let result = Math.min(this.contrast * image.ucharPtr(j, i)[c] + (this.brightness/1), 255);
          this.filteredImg.ucharPtr(j, i)[c] = Math.max(result, 0);
        }
      }
    }
    cv.imshow(id, this.filteredImg);
  }

  applyHSVChange(image, id, mask) {
    image.copyTo(this.filteredImg);
    cv.cvtColor(this.filteredImg, this.filteredImg, cv.COLOR_BGR2HSV, 0);

    for (let i = 0; i < image.cols; i++) {
      for (let j = 0; j < image.rows; j++) {
        let newHue = Math.min(this.filteredImg.ucharPtr(j, i)[0] + (this.hue/1), 180);
        this.filteredImg.ucharPtr(j, i)[0] = Math.max(newHue, 0);
        let newSaturation = Math.min(this.filteredImg.ucharPtr(j, i)[1] * (this.saturation/1), 255);
        this.filteredImg.ucharPtr(j, i)[1] = Math.max(newSaturation, 0);
        let newValue = Math.min(this.filteredImg.ucharPtr(j, i)[2] * (this.value/1), 255);
        this.filteredImg.ucharPtr(j, i)[2] = Math.max(newValue, 0);
      }
    }
    cv.cvtColor(this.filteredImg, this.filteredImg, cv.COLOR_HSV2BGR);
    cv.cvtColor(this.filteredImg, this.filteredImg, cv.COLOR_BGR2BGRA);

    cv.imshow(id, this.filteredImg);
    
  }


  

  deleteLayer() {
    this.componentRef.remove(this.index);
    this.filteredImg.delete();
  }



  
}
