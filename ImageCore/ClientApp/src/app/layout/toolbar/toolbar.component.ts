import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';
import { ImageProcessingService } from '../../services/image-processing.service';

@Component({
  selector: 'toolbar',
  templateUrl: './toolbar.component.html',
  styles: [`
    #toolbarContainer
    {
      width: 3vw;
      height: 100%;
      background-color: #C4B52C;
      display: flex;
      flex-direction:column;
      align-items: center;
      justify-content: flex-start;
      margin: 0px;
      padding-top: 15px;
    }
    .toolButton
    {
      width: 2.2vw;
      height: 2.2vw;
      background-color: #ffffff;
      margin: 3px;
      border-radius: 12px;
    }
    .selected
    {
      background-color: #272727 !important;
    }
    .colorPreviewContainer
    {
      position:relative;
      margin: 10px;
      width:2.2vw;
      height: 2.2vw;
    }
    .colorPreview
    {
      position: relative;
      width:60%;
      height: 60%;
      border-radius: 2px;
      border: 2px solid white;
    }
    .colorPreview:last-child{
      transform: translate(70%, -30%);
    }
  `]
})
export class ToolbarComponent{

  @ViewChild('toolbarContainer', { static: false }) toolbarContainer: ElementRef;

  constructor(private opencvService: ImageProcessingService) {
  }

  activateTool($event,tool: string) {
    this.opencvService.selectedTool = tool;
    let toolButtons = this.toolbarContainer.nativeElement.children;
    for (let i = 0; i < toolButtons.length; i++) {
      toolButtons[i].classList.remove("selected");
    }
    $event.target.classList.add("selected");
  }

  toggleActiveColor() {
    if (this.opencvService.activeColor == 0) {
      this.opencvService.activeColor = 1;
    }
    else {
      this.opencvService.activeColor = 0;
    }
    
  }

  
}
