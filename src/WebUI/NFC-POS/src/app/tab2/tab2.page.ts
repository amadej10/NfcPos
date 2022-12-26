import { Component, NgZone } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { NfcService } from '../services/nfc.service';
import { OperationsClient, PayCommand, UsersClient, UserVm } from '../web-api-client';
import { ToastController } from '@ionic/angular';
import { tap } from 'rxjs/operators';



export class MenuItem {
  public id: number;
  public price: number;
  public name: string;

  constructor(id: number, price: number, name: string) {
    this.id = id;
    this.price = price;
    this.name = name;
  }
}


@Component({
  selector: 'app-tab2',
  templateUrl: 'tab2.page.html',
  styleUrls: ['tab2.page.scss']
})
export class Tab2Page {

  nfcId: string;

  user$: Observable<UserVm>;
  user: UserVm;

  menuItems: MenuItem[] = [];
  counter: any = {}
  total: number;

  constructor(
    private nfcService: NfcService,
    private userClient: UsersClient,
    private operationsClient: OperationsClient,
    private toastController: ToastController,
    private ngZone: NgZone
  ) {
    this.menuItems.push(new MenuItem(1, 3, "Beer"));
    this.menuItems.push(new MenuItem(2, 3.5, "Wine"));
    this.menuItems.push(new MenuItem(3, 3.2, "Beer 2"));
    this.menuItems.push(new MenuItem(4, 2, "Water"));
    this.menuItems.push(new MenuItem(5, 3, "Juice"));
    this.menuItems.push(new MenuItem(6, 1.5, "Ice"));

    this.InitState();
    console.log(this.counter);


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

  getNfcId() {
    console.log(this.nfcId)
  }

  async presentToast(message) {
    const toast = await this.toastController.create({
      message: message,
      duration: 1500,
      position: 'top'
    });
    await toast.present();
  }

  addItem(id: number) {
    this.counter[id] += 1;
    // console.log(this.counter[id]);
    this.getTotal();
  }

  subtractItem(id: number) {
    if (this.counter[id] > 0) {
      this.counter[id] -= 1;
      // console.log(this.counter[id]);
      this.getTotal();
    }
  }

  getTotal() {
    this.total = 0;
    this.menuItems.forEach(item => {
      this.total += this.counter[item.id] * item.price;
    })

    this.total = Math.round(this.total * 100) / 100

    if (this.total > this.user.user.balance) {
      this.presentToast("Total is bigger than user balance :S");

    }
  }

  InitState() {
    this.menuItems.forEach(x => {
      this.counter[x.id] = 0;
    })
    this.total = 0;
  }

  pay() {
    this.operationsClient.pay(new PayCommand({
      nfcId: this.nfcId,
      totalPayAmount: Math.round(this.total * 100) / 100
    })).subscribe(result => {
      this.presentToast("Paid: " + this.total + " User has: " + result.newBalance)
      this.InitState();
      this.nfcId = undefined;
    },
      error => {
        console.log(error);
        this.presentToast("User can't pay. Balance is to low :S")
      })
  }

}

