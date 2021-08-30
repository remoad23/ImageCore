import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

function StartApp()
{
  platformBrowserDynamic(providers).bootstrapModule(AppModule)
    .catch(err => console.log(err));
}



let urlParams = new URLSearchParams(window.location.search);
let token = urlParams.get('token');

console.log(token);

fetch("https://localhost:5001/AuthApi", {
  method: 'GET',
  headers: {
    'Content-Type':  'application/json; charset=utf-8',
    'Accept':  'application/json',
    'Authorization': `${token}`
  }})
  .then(res =>{
    if(res.status !== 200)
    {
      providers[1].useValue = "NotValid";
      window.location.href = providers[2].useValue;
    }
    urlParams.set('token',"s");
    history.replaceState(null, null, "");
  }).then(StartApp)



const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
  { provide: 'TOKEN',useValue:token },
  { provide: 'REDIRECTION_URL' ,useValue: 'https://localhost:5001'}
];



if (environment.production) {
  enableProdMode();
}
