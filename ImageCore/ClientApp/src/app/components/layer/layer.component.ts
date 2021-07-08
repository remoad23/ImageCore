import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';
import { ImageProcessingService } from '../../services/image-processing.service';

@Component({
  selector: 'layer',
  templateUrl: './layer.component.html',
  styles: [`
    
  `]
})
export class LayerComponent{
  

  @ViewChild('layerView', { static: false }) layerView: ElementRef;
  @ViewChild('originalImgView', { static: false }) originalImgView: ElementRef;

  private originalImg: any;
  private processedImg: any;
  private mask: any;

  private imgSource;


  constructor(private ngOpenCVService: NgOpenCVService) {
    
  }

  ngAfterViewInit() {
    this.loadImage();
  }

  loadImage() {
    if (this.imgSource.target.files.length) {
      const reader = new FileReader();
      const load$ = fromEvent(reader, 'load');
      load$
        .pipe(
          switchMap(() => {
            return this.ngOpenCVService.loadImageToHTMLCanvas(`${reader.result}`, this.originalImgView.nativeElement);
          })
        )
        .subscribe(
          () => {
            this.originalImg = cv.imread(this.originalImgView.nativeElement.id);
            this.processedImg = new cv.Mat();
            this.originalImg.copyTo(this.processedImg);
            this.mask = new cv.Mat(this.originalImg.rows, this.originalImg.cols, this.originalImg.type(), new cv.Scalar(255, 255, 255, 255));
            cv.imshow(this.layerView.nativeElement.id, this.processedImg);
          },
          err => {
            console.log('Error loading image', err);
          }
      );
      reader.readAsDataURL(this.imgSource.target.files[0]);
    }
  }

  setImgSource(event) {
    this.imgSource = event;
  }

  getMats() {
    return [this.originalImg, this.processedImg, this.mask];
  }

  
}
