import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FullRegisterComponent } from './full-register.component';

describe('FullRegisterComponent', () => {
  let component: FullRegisterComponent;
  let fixture: ComponentFixture<FullRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FullRegisterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FullRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
