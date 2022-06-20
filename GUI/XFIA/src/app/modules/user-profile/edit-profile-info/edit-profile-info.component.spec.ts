import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProfileInfoComponent } from './edit-profile-info.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
describe('EditProfileInfoComponent', () => {
  let component: EditProfileInfoComponent;
  let fixture: ComponentFixture<EditProfileInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditProfileInfoComponent ],
      imports:[HttpClientTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditProfileInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
