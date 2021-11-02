import { Injectable } from '@angular/core';
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

  constructor(private opencvService: ImageProcessingService)
  {
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
  }

  updateData(text:string)
  {
    this.connection.send("Send",text);
  }

  reconnect()
  {

  }

  startConnection()
  {
    this.connection.start();
  }

  abortConnection()
  {
    this.connection.stop();
  }

  private initEvenets()
  {

  }
}
