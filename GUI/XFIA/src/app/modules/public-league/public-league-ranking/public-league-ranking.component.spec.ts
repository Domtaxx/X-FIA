import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicLeagueRankingComponent } from './public-league-ranking.component';

describe('PublicLeagueRankingComponent', () => {
  let component: PublicLeagueRankingComponent;
  let fixture: ComponentFixture<PublicLeagueRankingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublicLeagueRankingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicLeagueRankingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
