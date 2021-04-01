import { Component } from '@angular/core';
import {DataTransmitterServiceService} from "./services/data-transmitter-service.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  text = "App Text";

  constructor(private transmitter: DataTransmitterServiceService) {
  }

  changeText()
  {
    this.transmitter.updateData("text");
    this.text = "text";
  }
}
