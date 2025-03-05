import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { RegiaoService } from './regiao.service';

interface Cidade {
    id: number;
    nome: string;
    uf: string;
}

@Component({
    selector: 'app-cidade-selector',
    templateUrl: './cidade-selector.component.html',
    styleUrls: ['./cidade-selector.component.scss']
})
export class CidadeSelectorComponent implements OnInit {
    @Input() cidadeSelecionada: FormControl = new FormControl('');
    @Output() remover = new EventEmitter<void>();

    cidadesDisponiveis: Cidade[] = [];

    constructor(private regiaoService: RegiaoService) { }

    ngOnInit() {
        this.listarCidades();
    }

    listarCidades() {
        this.regiaoService.getCidades().subscribe(
            (data) => this.cidadesDisponiveis = data,
            (error) => console.error('Erro ao buscar cidades:', error)
        );
    }
}
