import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"

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

  constructor()
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

    this.connection.on('message',e => (document.getElementById('inn') as HTMLInputElement).value = e );
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
