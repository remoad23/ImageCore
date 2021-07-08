import { Injectable, Component, OnInit, ViewChild, AfterViewInit, ElementRef, ViewContainerRef, ComponentRef, ComponentFactoryResolver, ReflectiveInjector } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { DataTransmitterServiceService } from "./data-transmitter-service.service";
import { NgOpenCVService, OpenCVLoadResult } from 'ng-open-cv';
import { LayerComponent } from '../components/layer/layer.component';
import { ImageviewComponent } from '../layout/imageview/imageview.component';

@Injectable({
  providedIn: 'root'
})
/**
 * Class handles the image processing methods
 */
export class ImageProcessingService {

  // data from backend will be put in here
  private receivedData: any;

  // signalR connection
  private connection;

  private imgViewComponent;

  private layerArray: LayerComponent[];
  private factoryResolver: any;


  constructor(private transmitter: DataTransmitterServiceService, private ngOpenCVService: NgOpenCVService, private componentFactory: ComponentFactoryResolver)
  {
    this.factoryResolver = componentFactory;
    this.layerArray = [];
  }


  addLayer(event) {
    console.log(this.imgViewComponent);
    let newLayerFactory = this.factoryResolver.resolveComponentFactory(LayerComponent);
    let newLayer = this.imgViewComponent.createComponent(newLayerFactory);
    newLayer.instance.setImgSource(event);
  }


  setImgViewComponent(reference: any) {
    this.imgViewComponent = reference;
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
