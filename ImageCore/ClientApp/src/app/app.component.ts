import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { DataTransmitterServiceService } from "./services/data-transmitter-service.service";
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styles: [`
  #layoutContainer
  {
    display: flex;
    justify-content: flex-start;
    width: 100vw;
    height: 95vh;
  }
  `]
})
export class AppComponent implements OnInit {
  title = 'app';

  text = "App Text";

  // HTML Element references
  @ViewChild('fileInput', { read: ViewContainerRef,static:false }) fileInput: ElementRef;
  @ViewChild('canvasInput', {static: false }) canvasInput: ElementRef;
  @ViewChild('canvasOutput', {static: false }) canvasOutput: ElementRef;

  // Keep tracks of the ready
  openCVLoadResult: Observable<OpenCVLoadResult>;

  constructor(private transmitter: DataTransmitterServiceService, private ngOpenCVService: NgOpenCVService) {
  }

  ngOnInit() {
    this.openCVLoadResult = this.ngOpenCVService.isReady$;
  }

  loadImage(event) {
    if (event.target.files.length) {
      const reader = new FileReader();
      const load$ = fromEvent(reader, 'load');
      load$
        .pipe(
          switchMap(() => {
            console.log(`${reader.result}`);
            return this.ngOpenCVService.loadImageToHTMLCanvas(`${reader.result}`, this.canvasOutput.nativeElement);
          })
        )
        .subscribe(
          () => {
            const src = cv.imread(this.canvasOutput.nativeElement.id);
            const gray = new cv.Mat();
            cv.cvtColor(src, gray, cv.COLOR_RGBA2GRAY, 0);
            cv.imshow(this.canvasOutput.nativeElement.id, gray);
            src.delete();
            gray.delete();
          },
          err => {
            console.log('Error loading image', err);
          }
        );
      reader.readAsDataURL(event.target.files[0]);
    }
  }

  changeText()
  {
    this.transmitter.updateData("text");
    this.text = "text";
  }
}
