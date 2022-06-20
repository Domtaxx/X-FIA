import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProfileTeamComponent } from './edit-profile-team.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
describe('EditProfileTeamComponent', () => {
  let component: EditProfileTeamComponent;
  let fixture: ComponentFixture<EditProfileTeamComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditProfileTeamComponent ],
      imports:[HttpClientTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditProfileTeamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
