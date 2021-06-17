import { NgModule } from '@angular/core';
import {CommonModule} from "@angular/common";
import {Routes, RouterModule} from '@angular/router';
import {AuthGuard} from "./Guards/auth.guard";
import {AppComponent} from "./app.component";

const routes: Routes = [

  {
    path: 'Project',component: AppComponent, canActivate: [AuthGuard],

  },
];


@NgModule({
  imports: [CommonModule,RouterModule.forRoot(routes)],
  exports: [CommonModule,RouterModule],
  declarations: []
})
export class AppRoutingModule { }
