import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MediaTypeListComponent } from './media-type-list.component';

describe('MediaTypeListComponent', () => {
  let component: MediaTypeListComponent;
  let fixture: ComponentFixture<MediaTypeListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MediaTypeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MediaTypeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
