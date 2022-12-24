import { Component, NgZone } from '@angular/core';
import { ToastController } from '@ionic/angular';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { NfcService } from '../services/nfc.service';
import { OperationsClient, TopUpCommand, UsersClient, UserVm } from '../web-api-client';

@Component({
  selector: 'app-tab3',
  templateUrl: 'tab3.page.html',
  styleUrls: ['tab3.page.scss']
})
export class Tab3Page {

  nfcId: string;
  user$: Observable<UserVm>;
  user: UserVm;
  topUpAmount: number;

  constructor(
    private nfcService: NfcService,
    private userClient: UsersClient,
    private operationsClient: OperationsClient,
    private toastController: ToastController,
    private ngZone: NgZone
  ) {

    nfcService.getId().subscribe((nfcId) => {
      this.nfcId = nfcId;
      console.log(nfcId, this.nfcId)
      if (nfcId) {
        this.presentToast("NFC tag scanned with id: " + nfcId);

        this.ngZone.run(() => {
          this.user$ = this.userClient.getUser(nfcId).pipe(tap(x => this.user = x));
        });

      }
    })
  }

  topUp() {
    this.operationsClient.topUp(new TopUpCommand({ topUpAmount: this.topUpAmount, nfcId: this.nfcId })).subscribe(result => {
      this.presentToast("New user balance is " + result.newBalance);
      this.initState();
      console.log("new balance "+result.newBalance);
    }, error => {
      console.error(error);
    })
  }

  async presentToast(message) {
    const toast = await this.toastController.create({
      message: message,
      duration: 1500,
      position: 'top'
    });
    await toast.present();
  }

  initState() {
    this.ngZone.run(() => {
      this.nfcId = undefined;
      this.topUpAmount = undefined;
    });
  }


}
