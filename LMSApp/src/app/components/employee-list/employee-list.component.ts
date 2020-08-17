import { Component, OnInit, ViewChild } from '@angular/core';
import { first } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import {UserService} from '../../_services/user.service';
import {User} from '../../_models/user';

import {EmployeeService} from '../../_services/employee.service';
import {Employee} from '../../_models/employee';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit {

  loading= false;
  users: User[];

  constructor(private userService: UserService,
    private employeeService: EmployeeService, 
    private router: Router,
    private formBuilder: FormBuilder) { }


  ngOnInit(): void {
    this.loading = true;
    this.employeeService.getUserList().pipe(first()).subscribe(users => {
      this.loading = false;
      this.users = users;
    });

    this.registerUser = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required]
    });
  }

  registerUser: FormGroup;
  submitted = false;

  get f() { return this.registerUser.controls; }
  @ViewChild('closebutton') closebutton;
  @ViewChild('closebutton2') closebutton2;


  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerUser.invalid) {
        return;
    }

    let myUser: User = new User;
    myUser.userName = this.registerUser.value.username;
    myUser.password = this.registerUser.value.password;
    myUser.firstName = this.registerUser.value.firstName;
    myUser.lastName = this.registerUser.value.lastName;

    let id = (<HTMLInputElement>document.getElementById("userId")).value;

    if(id === '')
    {
      this.employeeService.addUser(myUser).pipe(first())
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
      this.userService.editUser(id,myUser).pipe(first())
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

  updateUser(id: string){
    this.loading = true;
    this.userService.getUser(id).pipe(first()).subscribe(type => {
      this.loading = false;
      document.getElementById("btnAddUserModal").click();
      this.registerUser.setValue({
        firstName: type.firstName,
        username: type.userName, 
        password: '************', 
        lastName: type.lastName, 
      });
      (<HTMLInputElement>document.getElementById("userId")).value = type.id;
      (<HTMLInputElement>document.getElementById("password")).readOnly = true;
      (<HTMLInputElement>document.getElementById("username")).readOnly = true;


    });

  }

  resetPassword(id: string){
    (<HTMLInputElement>document.getElementById("userId")).value = id;
    document.getElementById("btnChangePassword").click();

  }

  resetPasswordSubmit()
  {
    let id = (<HTMLInputElement>document.getElementById("userId")).value;
    let password = (<HTMLInputElement>document.getElementById("changePasswordtext")).value;

    if(password != undefined && password != ''){
      this.userService.resetPassword(id,password).pipe(first())
      .subscribe(
        data => {
          this.ngOnInit();
        },
        error => {
          console.log(error);
        });
    }
    else{
      alert("Password is empty");
    }

    this.closebutton2.nativeElement.click(); 

  }
  deleteUser(id: string){
    this.userService.deleteUser(id).pipe(first())
    .subscribe(
      data => {
        this.ngOnInit();
      },
      error => {
        console.log(error);
      });
      
  }

  clearModal(){
    (<HTMLInputElement>document.getElementById("username")).value = '';
    (<HTMLInputElement>document.getElementById("password")).value = '';
    (<HTMLInputElement>document.getElementById("firstName")).value = '';
    (<HTMLInputElement>document.getElementById("lastName")).value = '';
    (<HTMLInputElement>document.getElementById("userId")).value = '';
    (<HTMLInputElement>document.getElementById("password")).readOnly = false;
    (<HTMLInputElement>document.getElementById("username")).readOnly = false;
  }
}
