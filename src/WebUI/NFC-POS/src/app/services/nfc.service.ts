import { Injectable } from '@angular/core';
import { NFC, Ndef, NfcTag } from '@awesome-cordova-plugins/nfc/ngx';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Observable } from 'rxjs/internal/Observable';

@Injectable()
export class NfcService {
    private readerMode$;

    private tagId: BehaviorSubject<string>;

    constructor(private nfc: NFC, private ndef: Ndef,) {
        this.tagId = new BehaviorSubject<string>("");
        this.nfcGetId();

    }

    getId(): Observable<string>{
        return this.tagId.asObservable();
    }

    private setId(newId:string): void{
        this.tagId.next(newId)
    }

    nfcGetId() {
        let flags = this.nfc.FLAG_READER_NFC_A | this.nfc.FLAG_READER_NFC_V;
        this.readerMode$ = this.nfc.readerMode(flags).subscribe(
            tag => {
                let tagId = this.nfc.bytesToHexString(tag.id);
                this.setId(tagId);
                // console.log(this.tagId, tag);
                console.log("Scanned new tagID:",tagId);
            },
            err => console.log('Error reading tag', err)
        );
    }
}