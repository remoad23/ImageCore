import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';

@Component({
  selector: 'propertybar',
  templateUrl: './propertybar.component.html',
  styles: [`
    #propertybarContainer
    {
      width: 15vw;
      height: 95vh;
      background-color: #C4B52C;
      border-left: 2px solid #272727;
      display: flex;
      flex-direction:column;
      align-items: center;
      justify-content: flex-start;
      margin: 0px;
    }
    .propertyContainer
    {
      width: 100%;
      padding: 10%;
      border-bottom: 2px solid #272727;
    }
    .colorContainer
    {
      display: flex;
      flex-direction: row;
      justify-content: flex-start;
      flex-shrink: 0;
      width: 100%;
    }
    .colorInputContainer
    {
      display: flex;
      flex-direction: column;
      width: 40%;
    }
    .propertyInputContainer
    {
      display: flex;
      flex-direction: column;
      width: 100%;
    }
    .filterContainer
    {
      display: flex;
      flex-direction: row;
      width: 100%;
      flex-wrap: wrap;
    }
    .layerContainer
    {
      width: 100%;
      height: 150px;
      overflow-y: scroll;
    }
    .filterButton
    {
      width: 40px;
      height: 40px;
      background-color: #ffffff;
      margin: 5px;
      border-radius: 10px;
    }
    input[type="number"]
    {
      width: 60%;
    }
    #pickArea
    {
      width: 100px;
      height: 100px;
      background-color: rgb(255,0,0);
      background: -webkit-linear-gradient(top, hsl(0, 0%, 100%) 0%, hsla(0, 0%, 100%, 0) 50%, hsla(0, 0%, 0%, 0) 50%, hsl(0, 0%, 0%) 100%), -webkit-linear-gradient(left, hsl(0, 0%, 50%) 0%, hsla(0, 0%, 50%, 0) 100%);
    }
  `]
})
export class PropertybarComponent{
  

  constructor() {
  }

  
}
