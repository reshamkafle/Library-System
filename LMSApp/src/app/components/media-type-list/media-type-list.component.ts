import { Component, OnInit, ViewChild } from '@angular/core';
import { first } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import {MediaTypeService} from '../../_services/media-type.service';
import {MediaType} from '../../_models/mediaType';

@Component({
  selector: 'app-media-type-list',
  templateUrl: './media-type-list.component.html',
  styleUrls: ['./media-type-list.component.scss']
})
export class MediaTypeListComponent implements OnInit {

  loading= false;
  types: MediaType[];

  constructor(private mediaTypeService: MediaTypeService, 
    private router: Router,
    private formBuilder: FormBuilder) { }


  ngOnInit(): void {
    this.loading = true;
    this.mediaTypeService.getMediaTypeList().pipe(first()).subscribe(types => {
      this.loading = false;
      this.types = types;
    });

    this.registerMediaType = this.formBuilder.group({
      name: ['', Validators.required],
    });
  }

  registerMediaType: FormGroup;
  submitted = false;

  get f() { return this.registerMediaType.controls; }
  @ViewChild('closebutton') closebutton;

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerMediaType.invalid) {
        return;
    }

    let myMediaType: MediaType = new MediaType;
    myMediaType.name = this.registerMediaType.value.name;

    let id = (<HTMLInputElement>document.getElementById("typeId")).value;

    if(id === '')
    {
      this.mediaTypeService.addMediaType(myMediaType).pipe(first())
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
      this.mediaTypeService.editMediaType(id,myMediaType).pipe(first())
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

  updateMediaType(id: string){
    this.loading = true;
    this.mediaTypeService.getMediaType(id).pipe(first()).subscribe(type => {
      this.loading = false;
      document.getElementById("btnAddMediaTypeModal").click();
      this.registerMediaType.setValue({
        name: type.name, 
      });
      (<HTMLInputElement>document.getElementById("typeId")).value = type.id;
      
    });

  }

  deleteMediaType(id: string){
    this.mediaTypeService.deleteMediaType(id).pipe(first())
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
    (<HTMLInputElement>document.getElementById("typeId")).value = '';
  }

}
