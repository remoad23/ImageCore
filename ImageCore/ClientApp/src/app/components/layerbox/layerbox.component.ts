import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';

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
      width: 20%;
      background-color: #ffffff;
      border: 1px solid black;
      margin-right:10px;
    }
    #layername
    {
      color: #ffffff;
      font-size: 13px;
    }
  `]
})
export class LayerboxComponent{
  

  constructor() {
  }

  
}
