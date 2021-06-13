import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';

@Component({
  selector: 'toolbar',
  templateUrl: './toolbar.component.html',
  styles: [`
    #toolbarContainer
    {
      width: 4vw;
      height: 100%;
      background-color: #C4B52C;
      display: flex;
      flex-direction:column;
      align-items: center;
      justify-content: flex-start;
      margin: 0px;
    }
    .toolButton
    {
      width: 3vw;
      height: 3vw;
      background-color: #ffffff;
      margin: 3px;
      border-radius: 12px;
    }
  `]
})
export class ToolbarComponent{
  

  constructor() {
  }

  
}
