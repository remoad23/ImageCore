import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';

@Component({
  selector: 'imageview',
  templateUrl: './imageview.component.html',
  styles: [`
    #imageviewContainer
    {
      width: 81vw;
      height: 95vh;
      background-color:#272727;
      overflow: scroll;
    }
    .menuButton
    {
      color:#5D5D5D;
      border-radius: 12px;
    }
  `]
})
export class ImageviewComponent{
  

  constructor() {
  }

  
}
