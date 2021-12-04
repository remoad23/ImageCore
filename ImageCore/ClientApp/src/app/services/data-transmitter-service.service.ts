import { HttpHeaders } from '@angular/common/http';
import {Inject, Injectable} from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { ImageProcessingService } from '../services/image-processing.service';
import { HttpClient} from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
/**
 * Class is used as a objectification of the connection between angular and SignalR
 */
export class DataTransmitterServiceService {

  // data from backend will be put in here
  private receivedData: any;

  // signalR connection
  private connection;
  private http: HttpClient;
  private baseUrl: string;
  private pId: string;
  private token: string;

  constructor(private opencvService: ImageProcessingService, private client: HttpClient, @Inject("BASE_BACKEND_URL") _baseUrl: string,
    @Inject("P_ID") _pId,
    @Inject("TOKEN") _token)
  {
      this.pId = _pId;
      this.http = client;
      this.baseUrl = _baseUrl;
      this.token = _token;
    this.receivedData = [];

    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:5001/Chathub",{
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .withAutomaticReconnect()
      .build();
    this.startConnection();
    this.connection.on('message', e => {

      var array = e.split(',');
      if (array[0] == "addGeometryLayer") {
        this.opencvService.addGeometryLayer(array[1], array[2], array[3], array[4], array[5], array[6]);
      }
      else if (array[0] == "moveLayer") {
        this.opencvService.layerArray[array[3]].viewLeft = parseFloat(array[1]);
        this.opencvService.layerArray[array[3]].viewTop = parseFloat(array[2]);
      }

    });

    this.connection.on('RegisterSession', e => {
      console.log(e)
    });



    this.connection.on('NewImageUploaded', e => this.getImage(e));

  }

  updateData(text:string)
  {
    console.log("triggered");
    this.connection.send("Send",text,this.pId);

  }

  notifyNewImageUploaded(imageId: string)
  {
    this.connection.send("NotifyNewImageUploaded",imageId,this.pId);
  }

  async saveProject() {
    
    let stringifiedData = "";
    //this.connection.send("SaveProject", projectData, this.pId);
    const formData = new FormData();
    for (let layer of this.opencvService.layerArray) {
      var projectData = {
        ImageLayerId: layer.layerId,
        Width: layer.width,
        Height: layer.height,
        Masked: layer.masked,
        Rotation: layer.rotationAngle,
        ProcessedImageMat: this.opencvService.stringifyMat(layer.processedImg),
        OriginalImageMat: this.opencvService.stringifyMat(layer.originalImg),
        MaskMat: this.opencvService.stringifyMat(layer.mask),
        LayerColor: layer.layerColor,
        FontSize: layer.fontSize,
        FontStrength: layer.fontStrength,
        Text: layer.text,
        FilterId: 0,
        Name: "",
        X: layer.viewLeft,
        Y: layer.viewTop,
        Z: layer.index,
        Opacity: layer.opacity,
        Visible: !layer.isHidden,
        LayerType: layer.layerType,
        ProjectId: this.pId
      };
      
      stringifiedData = JSON.stringify(projectData);
      formData.append('projectData[]', stringifiedData);
      let guid = (layer.layerId != null) ? layer.layerId : '00000000-0000-0000-0000-000000000000';
      console.log(guid);
      formData.append('imageLayerId[]', guid);
      
    }

    const headers = new HttpHeaders()
      .set("Authorization", this.token);

    const httpOptions = { headers: headers };

    await this.http.post<any>(this.baseUrl + "saveproject", formData, httpOptions).subscribe(

      (res) => {
        let result = res;
        for (let i = 0; i < this.opencvService.layerArray.length; i++) {
          this.opencvService.layerArray[i].layerId = result[i];
        }
      },
      (err) => console.log(err)
    );
  }

  async saveLayer(index: number) {

    //this.connection.send("SaveProject", projectData, this.pId);
    const formData = new FormData();
    var projectData = {
      ImageLayerId: this.opencvService.layerArray[index].layerId,
      Width: this.opencvService.layerArray[index].width,
      Height: this.opencvService.layerArray[index].height,
      Masked: this.opencvService.layerArray[index].masked,
      Rotation: this.opencvService.layerArray[index].rotationAngle,
      ProcessedImageMat: this.opencvService.stringifyMat(this.opencvService.layerArray[index].processedImg),
      OriginalImageMat: "",
      MaskMat: (this.opencvService.layerArray[index].masked) ? this.opencvService.stringifyMat(this.opencvService.layerArray[index].mask) : "",
      LayerColor: this.opencvService.layerArray[index].layerColor,
      FontSize: this.opencvService.layerArray[index].fontSize,
      FontStrength: this.opencvService.layerArray[index].fontStrength,
      Text: this.opencvService.layerArray[index].text,
      FilterId: 0,
      Name: "",
      X: this.opencvService.layerArray[index].viewLeft,
      Y: this.opencvService.layerArray[index].viewTop,
      Z: this.opencvService.layerArray[index].index,
      Opacity: this.opencvService.layerArray[index].opacity,
      Visible: !this.opencvService.layerArray[index].isHidden,
      LayerType: this.opencvService.layerArray[index].layerType,
      ProjectId: this.pId
    };

    let stringifiedData = JSON.stringify(projectData);
    formData.append('projectData', stringifiedData);
    let guid = (this.opencvService.layerArray[index].layerId != null) ? this.opencvService.layerArray[index].layerId : '00000000-0000-0000-0000-000000000000';
    formData.append('imageLayerId', guid);

    const headers = new HttpHeaders()
      .set("Authorization", this.token);

    const httpOptions = { headers: headers };

    await this.http.post<any>(this.baseUrl + "savelayer", formData, httpOptions).subscribe(

      (res) => {
        let result = res;
        this.opencvService.layerArray[index].layerId = result;
      },
      (err) => console.log(err)
    );
  }

  async loadProject() {
    const headers = new HttpHeaders()
      .set("responseType", "application/json")
      .set("Authorization", this.token);

    const httpOptions = { headers: headers };

    await this.http.get<any>(this.baseUrl + "getProject?projectId=" + this.pId, httpOptions).subscribe(

      (res) => {
        let result = res;
        this.opencvService.loadFromDB(result);

      },
      (err) => console.log(err)
    );
  }


  reconnect()
  {

  }

  async startConnection()
  {
    await this.connection.start();
    await this.connection.send("RegisterSession",this.pId);
  }

  abortConnection()
  {
    this.connection.stop();
  }

  private initEvenets()
  {

  }

  private async getImage(imageId: string) {
    const headers = new HttpHeaders()
      .set("responseType", "application/json")
      .set("Authorization", this.token);

    const httpOptions = {headers: headers};

    await this.http.get<any>(this.baseUrl + "getimagelayer?fileId=" + imageId, httpOptions).subscribe(

      (res) => {
        let result = res;
        if (result) {
          this.opencvService.addLayer(result);
        }

      },
      (err) => console.log(err)
    );
  }
}
