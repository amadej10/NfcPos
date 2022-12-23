import { Component, NgZone, OnInit, ViewChild } from '@angular/core';
import { IonModal } from '@ionic/angular';
import { OverlayEventDetail } from '@ionic/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CreateUserCommand, UsersClient, UsersVm, WeatherForecast, WeatherForecastClient } from '../web-api-client';
import { NfcService } from '../services/nfc.service';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})
export class Tab1Page implements OnInit {
  @ViewChild(IonModal) modal: IonModal;

  public usersData$: Observable<UsersVm>;

  name: string;
  surname: string;
  description: string;
  balance: number;
  nfcId: string;

  constructor(private userClient: UsersClient, private nfcService: NfcService, private ngZone: NgZone) {

  }
  ngOnInit(): void {
    this.getUsersData();
    this.nfcService.getId().subscribe((nfcId) => {      
      this.ngZone.run(() => {
        this.nfcId=nfcId;
      });
      console.log(nfcId, this.nfcId)
    })
  }


  getUsersData() {
    this.usersData$ = this.userClient.getUsers().pipe(map(x => new UsersVm({ users: x.users.sort((a, b) => (a.name > b.name) ? 1 : (b.name > a.name) ? -1 : 0) })));
  }


  cancel() {
    this.modal.dismiss(null, 'cancel');
  }

  confirm() {

    this.userClient.createUser(new CreateUserCommand({
      name: this.name,
      surname: this.surname,
      description: this.description,
      balance: this.balance
    })).subscribe((result) => {
      console.log("user id:", result)
      this.modal.dismiss(null, 'confirm');
    },
      error => {
        console.error(error);
      });
  }

  onWillDismiss(event: Event) {
    const ev = event as CustomEvent<OverlayEventDetail<string>>;
    if (ev.detail.role === 'confirm') {
      this.getUsersData();
    }
    this.clearInputData();
  }


  willPresent(event: Event) {
    console.log("Before present");
    
  }

  clearInputData() {
    this.ngZone.run(() => {
      this.name = "";
      this.surname = "";
      this.nfcId = "";
      this.description = "";
      this.balance = undefined;
    })
  }
}
