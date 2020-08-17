import { Component, OnInit, ViewChild } from '@angular/core';
import { first } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import {MediaService} from '../../_services/media.service';
import {Media} from '../../_models/media';

import {MediaType} from '../../_models/mediaType';
import {MediaTypeService} from '../../_services/media-type.service';

import {Publisher} from '../../_models/publisher';
import {PublisherService} from '../../_services/publisher.service';

import {Author} from '../../_models/author';
import {AuthorService} from '../../_services/author.service';

@Component({
  selector: 'app-media-list',
  templateUrl: './media-list.component.html',
  styleUrls: ['./media-list.component.scss']
})
export class MediaListComponent implements OnInit {

  loading= false;
  medias: Media[];
  mediaTypes: MediaType[];
  publishers: Publisher[];
  authors: Author[];

  constructor(
    private mediaSevice: MediaService,
    private mediaTypeService: MediaTypeService,
    private publisherService: PublisherService,
    private authorService: AuthorService,
    private router: Router,
    private formBuilder: FormBuilder) { }


  ngOnInit(): void {
    this.loading = true;
    this.mediaSevice.getMediaList().pipe(first()).subscribe(medias => {
      this.loading = false;
      this.medias = medias;
    });
    
    this.mediaTypeService.getMediaTypeList().pipe(first()).subscribe(types => {
      this.loading = false;
      this.mediaTypes = types;
    });

    this.publisherService.getPublisherList().pipe(first()).subscribe(publishers => {
      this.loading = false;
      this.publishers = publishers;
    });
    this.authorService.getAuthorList().pipe(first()).subscribe(authors => {
      this.loading = false;
      this.authors = authors;
    });

    this.registerMedia = this.formBuilder.group({
      title: ['', Validators.required],
      authorId: ['', Validators.required],
      isbn: ['', Validators.required],
      year: ['', Validators.required],
      publisherId: ['', Validators.required],
      cost: ['', Validators.required],
      mediaTypeId: ['', Validators.required],
      totalQty: ['', Validators.required],
    });
  }

  registerMedia: FormGroup;
  submitted = false;

  get f() { return this.registerMedia.controls; }
  @ViewChild('closebutton') closebutton;

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerMedia.invalid) {
        return;
    }

    let myMedia: Media = new Media;
    myMedia.title = this.registerMedia.value.title;
    myMedia.authorId = this.registerMedia.value.authorId;
    myMedia.isbn = this.registerMedia.value.isbn;
    myMedia.year = this.registerMedia.value.year;
    myMedia.publisherId = this.registerMedia.value.publisherId;
    myMedia.cost = this.registerMedia.value.cost;
    myMedia.mediaTypeId = this.registerMedia.value.mediaTypeId;
    myMedia.totalQty = this.registerMedia.value.totalQty;

    let id = (<HTMLInputElement>document.getElementById("mediaId")).value;

    if(id === '')
    {
      this.mediaSevice.addMedia(myMedia).pipe(first())
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
      this.mediaSevice.editMedia(id,myMedia).pipe(first())
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

  updateMedia(id: string){
    this.loading = true;
    this.mediaSevice.getMedia(id).pipe(first()).subscribe(type => {
      this.loading = false;
      document.getElementById("btnAddMediaModal").click();
      this.registerMedia.setValue({
        title: type.title, 
        authorId: type.authorId,
        isbn: type.isbn,
        year: type.year,
        publisherId:type.publisherId,
        cost: type.cost,
        mediaTypeId: type.mediaTypeId,
        totalQty: type.totalQty
      });
      (<HTMLInputElement>document.getElementById("mediaId")).value = type.id;
      
    });

  }

  deleteMedia(id: string){
    this.mediaSevice.deleteMedia(id).pipe(first())
    .subscribe(
      data => {
        this.ngOnInit();
      },
      error => {
        console.log(error);
      });
      
  }

  clearModal(){
    (<HTMLInputElement>document.getElementById("title")).value = '';
    (<HTMLInputElement>document.getElementById("authorId")).value = '';
    (<HTMLInputElement>document.getElementById("isbn")).value = '';
    (<HTMLInputElement>document.getElementById("year")).value = '';
    (<HTMLInputElement>document.getElementById("publisherId")).value = '';
    (<HTMLInputElement>document.getElementById("cost")).value = '';
    (<HTMLInputElement>document.getElementById("mediaTypeId")).value = '';
    (<HTMLInputElement>document.getElementById("totalQty")).value = '';

  }

}
