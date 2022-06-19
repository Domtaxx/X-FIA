import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProfileTeamComponent } from './edit-profile-team.component';

describe('EditProfileTeamComponent', () => {
  let component: EditProfileTeamComponent;
  let fixture: ComponentFixture<EditProfileTeamComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditProfileTeamComponent ]
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
