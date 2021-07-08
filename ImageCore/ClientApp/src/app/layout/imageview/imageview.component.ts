import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef } from '@angular/core';
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
      width: 81vw;
      height: 95vh;
      background-color:#272727;
      overflow: scroll;
    }
  `]
})
export class ImageviewComponent{

  @ViewChild('imageviewContainer', { read: ViewContainerRef , static: false }) imageviewContainer: ViewContainerRef;

  constructor(private opencvService: ImageProcessingService) {
  }

  ngAfterViewInit() {
    this.opencvService.setImgViewComponent(this.imageviewContainer);
  }


  
}
