import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProfileComponent } from './edit-profile.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
describe('EditProfileComponent', () => {
  let component: EditProfileComponent;
  let fixture: ComponentFixture<EditProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditProfileComponent ],
      imports:[HttpClientTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
