import { Component, OnInit, ViewChild } from '@angular/core';
import { first } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import {PublisherService} from '../../_services/publisher.service';
import {Publisher} from '../../_models/publisher';


@Component({
  selector: 'app-publisher-list',
  templateUrl: './publisher-list.component.html',
  styleUrls: ['./publisher-list.component.scss']
})
export class PublisherListComponent implements OnInit {

  loading= false;
  publishers: Publisher[];

  constructor(private mediaTypeService: PublisherService, 
    private router: Router,
    private formBuilder: FormBuilder) { }


  ngOnInit(): void {
    this.loading = true;
    this.mediaTypeService.getPublisherList().pipe(first()).subscribe(publishers => {
      this.loading = false;
      this.publishers = publishers;
    });

    this.registerPublisher = this.formBuilder.group({
      name: ['', Validators.required],
    });
  }

  registerPublisher: FormGroup;
  submitted = false;

  get f() { return this.registerPublisher.controls; }
  @ViewChild('closebutton') closebutton;

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerPublisher.invalid) {
        return;
    }

    let myPublisher: Publisher = new Publisher;
    myPublisher.name = this.registerPublisher.value.name;

    let id = (<HTMLInputElement>document.getElementById("publisherId")).value;

    if(id === '')
    {
      this.mediaTypeService.addPublisher(myPublisher).pipe(first())
      .subscribe(
        data => {
          this.ngOnInit();
        },
        error => {
          console.log(error);
        });
    }
    else
    {
      this.mediaTypeService.editPublisher(id,myPublisher).pipe(first())
        .subscribe(
          data => {
            this.ngOnInit();
          },
          error => {
            console.log(error);
          });
    }   
    this.closebutton.nativeElement.click(); 
  }

  updatePublisher(id: string){
    this.loading = true;
    this.mediaTypeService.getPublisher(id).pipe(first()).subscribe(type => {
      this.loading = false;
      document.getElementById("btnAddPublisherModal").click();
      this.registerPublisher.setValue({
        name: type.name, 
      });
      (<HTMLInputElement>document.getElementById("publisherId")).value = type.id;
      
    });

  }

  deletePublisher(id: string){
    this.mediaTypeService.deletePublisher(id).pipe(first())
    .subscribe(
      data => {
        this.ngOnInit();
      },
      error => {
        console.log(error);
      });
      
  }

  clearModal(){
    (<HTMLInputElement>document.getElementById("name")).value = '';
    (<HTMLInputElement>document.getElementById("publisherId")).value = '';
  }

}
