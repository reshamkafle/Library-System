import { Component, OnInit } from '@angular/core';
import {HistoryService} from '../../_services/history.service';
import {History} from '../../_models/history';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss']
})
export class HistoryComponent implements OnInit {

  loading= false;
  checkout: History[];
  reserve: History[];


  constructor(private historyService: HistoryService) { }

  ngOnInit(): void {
    this.loading = true;
    this.historyService.getCheckoutHistory().pipe(first()).subscribe(checkout => {
      this.loading = false;
      this.checkout = checkout;
    });
    this.loading = true;
    this.historyService.getReservationHistory().pipe(first()).subscribe(reserve => {
      this.loading = false;
      this.reserve = reserve;
    });

  }

}
