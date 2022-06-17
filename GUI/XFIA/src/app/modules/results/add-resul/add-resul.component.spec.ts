import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddResulComponent } from './add-resul.component';

describe('AddResulComponent', () => {
  let component: AddResulComponent;
  let fixture: ComponentFixture<AddResulComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddResulComponent ]
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
