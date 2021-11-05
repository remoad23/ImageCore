import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef, Inject } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';
import { ImageProcessingService } from '../../services/image-processing.service';
import { DataTransmitterServiceService } from "../../services/data-transmitter-service.service";
import { HttpClient, HttpHeaders } from '@angular/common/http';

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
      margin-left: 0.5vw;
      background-image: url("../../assets/images/logo.svg");
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
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

  private http: HttpClient;
  private baseUrl: string;
  private pId: string;
  private token: string;

  constructor(
    private transmitter: DataTransmitterServiceService,
    private opencvService: ImageProcessingService,
    private client: HttpClient,
    @Inject("BASE_BACKEND_URL") _baseUrl: string,
    @Inject("P_ID") _pId,
    @Inject("TOKEN") _token  ) {
    this.pId = _pId;
    this.http = client;
    this.baseUrl = _baseUrl;
    this.token = _token;
  }


  async sendEvent(event) {

    //this.opencvService.addLayer(event);
    console.log(event.target.files[0]);

    const formData = new FormData();
    formData.append('File', event.target.files[0]);
    formData.append('ProjectId', this.pId)

    const headers = new HttpHeaders()
      .set("Authorization", this.token);

    const httpOptions = { headers: headers };

    await this.http.post<any>(this.baseUrl + "uploadimagelayer", formData,httpOptions).subscribe(

      (res) =>
      {
        let result = res;
        if (result) {
          this.transmitter.notifyNewImageUploaded(result);
        }
          
      },
      (err) => console.log(err)
    );

  }

  

  openFileMenu() {
    this.imageInput.nativeElement.click();
  }


  
}
