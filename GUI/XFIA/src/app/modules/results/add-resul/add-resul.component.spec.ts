import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddResulComponent } from './add-resul.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('AddResulComponent', () => {
  let component: AddResulComponent;
  let fixture: ComponentFixture<AddResulComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddResulComponent ],
      imports:[HttpClientTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddResulComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  

});
