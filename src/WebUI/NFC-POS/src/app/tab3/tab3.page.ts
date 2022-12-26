import { Component, NgZone } from '@angular/core';
import { ToastController } from '@ionic/angular';
import { EMPTY, Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
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
        this.ngZone.run(() => {
          this.user$ = this.userClient.getUser(nfcId)
            .pipe(
              tap(x => this.user = x),
              catchError((err, caught) => {
                this.nfcId = undefined;
                let response = JSON.parse(err.response);
                if (response.status == 404) {
                  this.presentToast("User with this NFC ID was not found.", 'middle');
                }
                console.error(response);
                console.error(caught);
                return EMPTY;
              }));
        });

      }
    })
  }

  topUp() {
    this.operationsClient.topUp(new TopUpCommand({ topUpAmount: this.topUpAmount, nfcId: this.nfcId })).subscribe(result => {
      this.presentToast("New user balance is " + result.newBalance);
      this.initState();
      console.log("new balance " + result.newBalance);
    }, error => {
      console.error(error);
    })
  }

  async presentToast(message, pos = 'top' as 'top' | 'bottom' | 'middle') {
    const toast = await this.toastController.create({
      message: message,
      duration: 1500,
      position: pos
    });
    await toast.present();
  }


  initState() {
    this.ngZone.run(() => {
      this.nfcId = undefined;
      this.topUpAmount = undefined;
    });
  }

  cancel() {
    this.initState();
  }


}
