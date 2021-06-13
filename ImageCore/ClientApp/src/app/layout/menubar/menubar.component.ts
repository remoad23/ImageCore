import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';

@Component({
  selector: 'menubar',
  templateUrl: './menubar.component.html',
  styles: [`
    #menubarContainer
    {
      width: 100%;
      height: 5vh;
      background-color: #C4B52C;
      border-bottom: 2px solid #272727;
      display: flex;
      flex-direction:row;
      align-items: center;
      justify-content: flex-start;
      margin: 0px;
    }
    #logo
    {
      width: 2vw;
      height: 2vw;
      background-color: #5D5D5D;
      margin: 3px;
      border-radius: 12px;
    }
    .menuButton
    {
      color:#ffffff;
      margin: 20px;
      border-radius: 12px;
    }
  `]
})
export class MenubarComponent{
  

  constructor() {
  }

  
}
