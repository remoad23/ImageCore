import {Inject, Injectable} from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { ImageProcessingService } from '../services/image-processing.service';


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
  private pId;

  constructor(private opencvService: ImageProcessingService,@Inject("P_ID") pId_: string)
  {
    this.pId = pId_;
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
      console.log("TRIGGERD     "+e);
      var array = e.split(',');
      if (array[0] == "addGeometryLayer") {
        this.opencvService.addGeometryLayer(array[1], array[2], array[3], array[4],array[5],array[6]);
      }

    });

    this.connection.on('RegisterSession', e => {
      console.log(e)
    });


  }

  updateData(text:string)
  {
    console.log("triggered");
    this.connection.send("Send",text,this.pId);

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
}
