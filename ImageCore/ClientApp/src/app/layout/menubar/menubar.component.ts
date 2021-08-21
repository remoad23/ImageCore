import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';
import { ImageProcessingService } from '../../services/image-processing.service';

@Component({
  selector: 'menubar',
  templateUrl: './menubar.component.html',
  styles: [`
    #menubarContainer
    {
      width: 100%;
      height: 5vh;
      background-color: #2b6777;
      display: flex;
      flex-direction:row;
      align-items: center;
      justify-content: flex-start;
      margin: 0px;
    }
    #logo
    {
      width: 4vh;
      height: 4vh;
      background-color: #5D5D5D;
      margin-left: 0.5vw;
      background-image: url("../../assets/images/photoshop-cc.svg");
      background-size: contain;
      background-repeat: no-repeat;
    }
    .menuButton
    {
      color:#ffffff;
      margin-left: 40px;
      border-radius: 12px;
    }
    .menuButton:hover{
      color:#52ab98;
      cursor: pointer;
    }
  `]
})
export class MenubarComponent{
  
  @ViewChild('imageInput', { static: false }) imageInput: ElementRef;

  constructor(private opencvService: ImageProcessingService) {

  }


  sendEvent(event) {
    this.opencvService.addLayer(event);
  }

  

  openFileMenu() {
    this.imageInput.nativeElement.click();
  }

  
}
