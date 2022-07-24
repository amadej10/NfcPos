import { Component } from '@angular/core';
import { WeatherForecast, WeatherForecastClient } from '../web-api-client';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})
export class Tab1Page {

  public forecasts: WeatherForecast[] = [];

  constructor(private client: WeatherForecastClient) {
    client.get().subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }

}
