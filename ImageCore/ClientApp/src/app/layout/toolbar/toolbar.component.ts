import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';
import { ImageProcessingService } from '../../services/image-processing.service';
import { DataTransmitterServiceService } from "../../services/data-transmitter-service.service";

/**
 * Holds the usable tools
 * */
@Component({
  selector: 'toolbar',
  templateUrl: './toolbar.component.html',
  styles: [`
    #toolbarContainer
    {
      width: 57px;
      height: 100%;
      background-color: #2b6777;
      display: flex;
      flex-direction:column;
      align-items: center;
      justify-content: flex-start;
      margin: 0px;
      padding-top: 15px;
      border-top: 2px solid #272727;
    }
    .toolButton
    {
      width: 42px;
      height: 42px;
      background-color: #ffffff;
      margin: 3px;
      border-radius: 12px;
      display: flex;
      justify-content: center;
      align-items: center;
    }
    .selected
    {
      background-color: #272727 !important;
    }
    .colorPreviewContainer
    {
      position:relative;
      margin: 10px;
      width:42px;
      height: 42px;
    }
    .colorPreview
    {
      position: relative;
      width:60%;
      height: 60%;
      border-radius: 2px;
      border: 2px solid#dbdbdb;
    }
    .colorPreview:last-child{
      transform: translate(70%, -30%);
    }
    .icon
    {
      pointer-events: none;
      width: 70%;
      height: 70%;
      text-align: center;
      display:flex;
      justify-content: center;
      align-items: center;
    }
    .mouseIcon{
      background-image: url("../../assets/images/mouseIcon.svg");
      background-size: contain;
      background-position: 60% 50%;
    }
    .rectangleIcon{
      background-image: url("../../assets/images/rectangle.svg");
      background-size: 80%;
      background-position: 58% 53%;
      background-repeat: no-repeat;
    }
    .textIcon{
      background-image: url("../../assets/images/textIcon.svg");
      background-size: 60%;
      background-position: 58% 53%;
      background-repeat: no-repeat;
    }
    .selectIcon{
      border: 2px dashed black;
      transform: translateX(1px);
      height:40% !important;
      width: 55% !important;
    }
    .toolButton:hover{
      background-color: #D9D9D9;
      cursor: pointer;
    }
  `]
})
export class ToolbarComponent{

  @ViewChild('toolbarContainer', { static: false }) toolbarContainer: ElementRef;

  constructor(private opencvService: ImageProcessingService, private transmitter: DataTransmitterServiceService) {
  }

  /**
   * activates the selected tool button
   * @param $event
   * @param tool
   */
  activateTool($event, tool: string) {
    $event.stopPropagation();
    this.opencvService.selectedTool = tool;
    let toolButtons = this.toolbarContainer.nativeElement.children;
    for (let i = 0; i < toolButtons.length; i++) {
      toolButtons[i].classList.remove("selected");
    }
    $event.target.classList.add("selected");
  }

  /**
   * toggles the active colors
   * */
  toggleActiveColor() {
    if (this.opencvService.activeColor == 0) {
      this.opencvService.activeColor = 1;
    }
    else {
      this.opencvService.activeColor = 0;
    }
    
  }

  
}
