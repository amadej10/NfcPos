import { Component } from '@angular/core';
import { NfcService } from '../services/nfc.service';

@Component({
  selector: 'app-tab2',
  templateUrl: 'tab2.page.html',
  styleUrls: ['tab2.page.scss']
})
export class Tab2Page {

  nfcId: string;
  constructor(private nfcService: NfcService) {
    nfcService.getId().subscribe((nfcId) => {
      this.nfcId=nfcId;
      console.log(nfcId, this.nfcId)
    })
  }

  getNfcId() {
    console.log(this.nfcId)
  }

}
