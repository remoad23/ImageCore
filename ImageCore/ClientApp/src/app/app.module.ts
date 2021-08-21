import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgOpenCVModule } from 'ng-open-cv';
import { OpenCVOptions } from 'ng-open-cv/public_api';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MenubarComponent } from './layout/menubar/menubar.component';
import { ToolbarComponent } from './layout/toolbar/toolbar.component';
import { PropertybarComponent } from './layout/propertybar/propertybar.component';
import { ImageviewComponent } from './layout/imageview/imageview.component';
import { LayerboxComponent } from './components/layerbox/layerbox.component';
import { LayerComponent } from './components/layer/layer.component';
import {AuthGuard} from "./Guards/auth.guard";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DragDropModule } from '@angular/cdk/drag-drop';


const openCVConfig: OpenCVOptions = {
  scriptUrl: `assets/opencv/wasm/3.4/opencv.js`,
  wasmBinaryFile: `./assets/opencv/wasm/3.4/opencv_js.wasm`,
  usingWasm: true
};

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ToolbarComponent,
    MenubarComponent,
    PropertybarComponent,
    ImageviewComponent,
    LayerboxComponent,
    LayerComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    NgOpenCVModule.forRoot(openCVConfig),
    HttpClientModule,
    FormsModule,
    DragDropModule,
    RouterModule.forRoot([
      {
        path: '',component: AppComponent, canActivate: [AuthGuard],

      },
    ]),
    BrowserAnimationsModule
  ],
  exports: [
    LayerComponent,
  ],
  entryComponents: [
    LayerComponent,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
