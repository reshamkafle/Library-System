import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import {MediaService} from '../../_services/media.service';
import {Media} from '../../_models/media';

import {Item} from '../../_models/item';
import {ReserveService} from '../../_services/reserve.service';
import {CheckoutService} from '../../_services/checkout.service';


@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.scss']
})
export class LibraryComponent implements OnInit {

  loading= false;
  medias: Media[];

  constructor( private mediaSevice: MediaService,
    private reserveService: ReserveService,
    private checkoutService: CheckoutService
    ) { }

  ngOnInit(): void {
    this.loading = true;
    this.mediaSevice.getMediaList().pipe(first()).subscribe(medias => {
      this.loading = false;
      this.medias = medias;
    });
  }

  reserve(id: string){

    let item: Item = new Item();
    item.mediaId = id;

    this.reserveService.reserve(item).pipe(first())
    .subscribe(
      data => {
        alert("Media is successfully reserved");
        this.ngOnInit();
      },
      error => {
        console.log(error);
      });
  }

  borrow(id: string){

    let item: Item = new Item();
    item.mediaId = id;
    
    this.checkoutService.checkout(item).pipe(first())
    .subscribe(
      data => {
        alert("Media is successfully borrowed");
        this.ngOnInit();
      },
      error => {
        console.log(error);
      });
  }
}
