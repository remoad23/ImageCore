import { Component, OnInit, ViewChild, Input, AfterViewInit, ElementRef, ViewContainerRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';

import { ImageProcessingService } from '../../services/image-processing.service';

/**
 * shows and manipulates the properties of an image layer
 * */
@Component({
  selector: 'propertybar',
  templateUrl: './propertybar.component.html',
  styles: [`
    #propertybarContainer
    {
      width: 16vw;
      height: 95vh;
      background-color: #2b6777;
      border-left: 2px solid #272727;
      display: flex;
      flex-direction:column;
      align-items: center;
      justify-content: flex-start;
      margin: 0px;
      color: #ffffff;
    }
    .propertyContainer
    {
      width: 100%;
      padding: 6%;
      border-top: 2px solid #272727;

    }
    #inputContainer{
      height: 400px;
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
      flex-wrap: wrap;
      width: 100%;
      max-height: 260px;
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
      overflow-y: auto;
      background-color: white;
      display:flex;
      flex-direction:column-reverse;
    }
    input[type="color"]{
      border: none;
      background-color: rgba(0,0,0,0);
      width: 2vw;
      height: 2vw;
    }
    #layerColor{
      height: 30px;
      width: 30px;
    }
    .filterButton
    {
      width: 40px;
      height: 40px;
      background-color: #ffffff;
      margin: 5px;
      border-radius: 10px;
      display: flex;
      justify-content: center;
      align-items: center;
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
    .contrastIcon{
      background-image: url("../../assets/images/contrastIcon.svg");
      background-size: contain;
      background-position: 58% 53%;
      background-repeat: no-repeat;
    }
    .hsvIcon{
      background-image: url("../../assets/images/hsvIcon.svg");
      background-size: 80%;
      background-position: 58% 53%;
      background-repeat: no-repeat;
    }
    .deleteIcon{
      background-image: url("../../assets/images/deleteIcon.svg");
      background-size: 80%;
      background-position: 58% 53%;
      background-repeat: no-repeat;
    }
    .filterButton:hover{
      background-color: #D9D9D9;
      cursor: pointer;
    }
    input[type="number"]
    {
      width: 30%;
      height: 20%;
    }
    input[type="text"]
    {
      width: 50%;
      height: 20%;
    }
    #pickArea
    {
      width: 100px;
      height: 100px;
      background-color: rgb(255,0,0);
      background: -webkit-linear-gradient(top, hsl(0, 0%, 100%) 0%, hsla(0, 0%, 100%, 0) 50%, hsla(0, 0%, 0%, 0) 50%, hsl(0, 0%, 0%) 100%), -webkit-linear-gradient(left, hsl(0, 0%, 50%) 0%, hsla(0, 0%, 50%, 0) 100%);
    }
    .cdk-drag-preview {
      height: 30px;
      width: 100px;
      overflow: hidden;
      box-sizing: border-box;
      border-radius: 4px;
      box-shadow: 0 5px 5px -3px rgba(0, 0, 0, 0.2),
                  0 8px 10px 1px rgba(0, 0, 0, 0.14),
                  0 3px 14px 2px rgba(0, 0, 0, 0.12);
    }

    .cdk-drag-placeholder {
      height: 30px;
      width: 100%;
      background-color: #272727;
    }

    .cdk-drag-animating {
      transition: transform 250ms cubic-bezier(0, 0, 0.2, 1);
    }

    .layerContainer.cdk-drop-list-dragging .layerBox:not(.cdk-drag-placeholder) {
      transition: transform 250ms cubic-bezier(0, 0, 0.2, 1);
    }

    .deleteButton
    {
      margin-top:2%;
      width:25px;
      height:25px;
      border-radius: 5px;
      background-color: white;
      display: flex;
      justify-content: center;
      align-items: center;
    }
    .deleteButton:hover{
      background-color: #D9D9D9;
      cursor: pointer;
    }
  `]
})
export class PropertybarComponent{

  @ViewChild('layerContainer', { static: false }) layerContainer: ElementRef;

  private mainColor = "255,0,0";

  public service: ImageProcessingService;
  constructor(private opencvService: ImageProcessingService) {
    this.service = opencvService;
  }

  /**
   * implements the drag and drop in the layer list and updates the order  
   * @param event
   */
  drop(event: CdkDragDrop<string[]>) {
    console.log(event.previousIndex, this.opencvService.layerArray.length - 1 - event.currentIndex);
    moveItemInArray(this.opencvService.layerArray, event.previousIndex, this.opencvService.layerArray.length - 1 - event.currentIndex);
    this.opencvService.updateLayerArray();
    this.opencvService.setActiveLayer(this.opencvService.layerArray.length - 1 - event.currentIndex);
  }

  /**
   * changes the x position of a layer
   * @param event
   */
  changeX(event) {
    this.opencvService.layerArray[this.opencvService.activeLayer].viewLeft = event.target.valueAsNumber + this.opencvService.layerArray[this.opencvService.activeLayer].viewCenterX;
  }

  /**
   * changes the y position of a layer
   * @param event
   */
  changeY(event) {
    this.opencvService.layerArray[this.opencvService.activeLayer].viewTop = event.target.valueAsNumber * -1 + this.opencvService.layerArray[this.opencvService.activeLayer].viewCenterY;
  }

  /**
   * changes the width of a layer
   * @param event
   */
  changeWidth(event) {
    this.opencvService.layerArray[this.opencvService.activeLayer].scaleImage(event.target.valueAsNumber, this.opencvService.layerArray[this.opencvService.activeLayer].height);
  }
  /**
   * changes the height of a layer
   * @param event
   */
  changeHeight(event) {
    this.opencvService.layerArray[this.opencvService.activeLayer].scaleImage(this.opencvService.layerArray[this.opencvService.activeLayer].width, event.target.valueAsNumber);
  }

  /**
   * changes the selected tool color
   * @param event
   */
  changeToolColor(event, idx) {
    this.opencvService.toolColor[idx] = event.target.value;
    let rgb = this.opencvService.hexToRgb(event.target.value);
    if (idx == 0) {
      this.mainColor = rgb[0] + "," + rgb[1] + "," + rgb[2];
    }
  }

  /**
   * changes the font size of a layer
   * @param event
   */
  changeFontSize(event) {
    this.opencvService.layerArray[this.opencvService.activeLayer].fontSize = event.target.valueAsNumber;
    this.opencvService.layerArray[this.opencvService.activeLayer].updateGeometry();
  }

  /**
   * changes the font strength of a layer
   * @param event
   */
  changeFontStrength(event) {
    this.opencvService.layerArray[this.opencvService.activeLayer].fontStrength = event.target.valueAsNumber;
    this.opencvService.layerArray[this.opencvService.activeLayer].updateGeometry();
  }

  /**
   * changes the text of a layer
   * @param event
   */
  changeText(event) {
    this.opencvService.layerArray[this.opencvService.activeLayer].text = event.target.value;
    this.opencvService.layerArray[this.opencvService.activeLayer].updateGeometry();
  }

  /**
   * changes the color of a layer
   * @param event
   */
  changeLayerColor(event) {
    this.opencvService.layerArray[this.opencvService.activeLayer].layerColor = event.target.value;
    this.opencvService.layerArray[this.opencvService.activeLayer].updateGeometry();
  }

  /**
   * changes the brightness of a filter
   * @param event
   */
  changeBrightness(event) {
    this.opencvService.layerArray[this.opencvService.activeLayer].brightness = event.target.value;
    this.opencvService.layerArray[this.opencvService.activeLayer-1].applyFilter();
  }

  /**
   * changes the contrast of a filter
   * @param event
   */
  changeContrast(event) {
    this.opencvService.layerArray[this.opencvService.activeLayer].contrast = event.target.value;
    this.opencvService.layerArray[this.opencvService.activeLayer - 1].applyFilter();
  }

  /**
   * changes the hue value of a filter
   * @param event
   */
  changeHue(event) {
    this.opencvService.layerArray[this.opencvService.activeLayer].hue = event.target.value;
    this.opencvService.layerArray[this.opencvService.activeLayer - 1].applyFilter();
  }

  /**
   * changes the saturation of a filter
   * @param event
   */
  changeSaturation(event) {
    this.opencvService.layerArray[this.opencvService.activeLayer].saturation = event.target.value;
    this.opencvService.layerArray[this.opencvService.activeLayer - 1].applyFilter();
  }

  /**
   * changes the value of a filter
   * @param event
   */
  changeValue(event) {
    this.opencvService.layerArray[this.opencvService.activeLayer].value = event.target.value;
    this.opencvService.layerArray[this.opencvService.activeLayer - 1].applyFilter();
  }



  
}
