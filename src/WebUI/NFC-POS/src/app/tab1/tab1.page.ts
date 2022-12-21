import { Component, OnInit, ViewChild } from '@angular/core';
import { IonModal } from '@ionic/angular';
import { OverlayEventDetail } from '@ionic/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CreateUserCommand, UsersClient, UsersVm, WeatherForecast, WeatherForecastClient } from '../web-api-client';
// import { NFC, Ndef, NfcTag } from '@awesome-cordova-plugins/nfc/ngx';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})
export class Tab1Page implements OnInit {
  @ViewChild(IonModal) modal: IonModal;

  public usersData$: Observable<UsersVm>;
  public readerMode$: any;

  name: string;
  surname: string;
  description: string;
  balance: number;
  tag: string;

  // private nfc: NFC, private ndef: Ndef
  constructor(private userClient: UsersClient,) {

  }
  ngOnInit(): void {
    this.getUsersData();

    // let flags = this.nfc.FLAG_READER_NFC_A | this.nfc.FLAG_READER_NFC_V;
    // this.readerMode$ = this.nfc.readerMode(flags).subscribe(
    //   tag => {
    //     this.tag = JSON.stringify(tag);
    //     console.log(tag);
    //   },
    //   err => console.log('Error reading tag', err)
    // );
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
  }
}
