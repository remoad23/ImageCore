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
        this.opencvService.addGeometryLayer(array[1], array[2], array[3], array[4],array[5],array[6]);
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
