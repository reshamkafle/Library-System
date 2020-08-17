import { Component, OnInit, ViewChild } from '@angular/core';
import { first } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import {AuthorService} from '../../_services/author.service';
import {Author} from '../../_models/author';

@Component({
  selector: 'app-author-list',
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.scss']
})
export class AuthorListComponent implements OnInit {

  loading= false;
  authors: Author[];

  constructor(private authorService: AuthorService, 
    private router: Router,
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.loading = true;
    this.authorService.getAuthorList().pipe(first()).subscribe(authors => {
      this.loading = false;
      this.authors = authors;
    });

    this.registerAuthor = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
    });
  }

  registerAuthor: FormGroup;
  submitted = false;

  get f() { return this.registerAuthor.controls; }
  @ViewChild('closebutton') closebutton;

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerAuthor.invalid) {
        return;
    }

    let myAuthor: Author = new Author;
    myAuthor.firstName = this.registerAuthor.value.firstName;
    myAuthor.lastName = this.registerAuthor.value.lastName;

    let id = (<HTMLInputElement>document.getElementById("authorId")).value;

    if(id === '')
    {
      this.authorService.addAuthor(myAuthor).pipe(first())
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
      this.authorService.editAuthor(id,myAuthor).pipe(first())
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

  updateAuthor(id: string){
    this.loading = true;
    this.authorService.getAuthor(id).pipe(first()).subscribe(author => {

      this.loading = false;
      document.getElementById("btnAddAuthorModal").click();
      this.registerAuthor.setValue({
        firstName: author.firstName, 
        lastName: author.lastName
      });
      (<HTMLInputElement>document.getElementById("authorId")).value = author.id;
      
    });

  }

  deleteAuthor(id: string){
    this.authorService.deleteAuthor(id).pipe(first())
    .subscribe(
      data => {
        this.ngOnInit();
      },
      error => {
        console.log(error);
      });
      
  }

  clearModal(){
    (<HTMLInputElement>document.getElementById("firstName")).value = '';
    (<HTMLInputElement>document.getElementById("lastName")).value = '';
    (<HTMLInputElement>document.getElementById("authorId")).value = '';
  }

}
