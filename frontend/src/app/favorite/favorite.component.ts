import { Component, OnInit } from '@angular/core';
import { FavoriteService } from '../services/favorite.service';

@Component({
  selector: 'app-favorite',
  templateUrl: './favorite.component.html',
  styleUrls: ['./favorite.component.scss'],
})
export class FavoriteComponent implements OnInit {
  favorites: Array<{ id: number; title: string; link: string }> = [];

  constructor(private favoriteService: FavoriteService) {}

  ngOnInit(): void {
    this.favoriteService.getAll().subscribe(async (response) => {
      this.favorites.push(...response);
    });
  }

  remove(id: number) {
    this.favoriteService.remove(id).subscribe(() => {
      this.favorites = this.favorites.filter((favorite) => favorite.id !== id);
    });
  }
}
