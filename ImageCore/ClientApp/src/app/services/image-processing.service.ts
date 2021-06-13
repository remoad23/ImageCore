import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
/**
 * Class is used as a objectification of the connection between angular and SignalR
 */
export class ImageProcessingService {

  // data from backend will be put in here
  private receivedData: any;

  // signalR connection
  private connection;

  constructor()
  {

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
