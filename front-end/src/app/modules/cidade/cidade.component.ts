import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CidadeService } from './cidade.service';

interface Cidade {
    id?: number;
    nome: string;
    uf: string;
}

@Component({
    selector: 'app-cidade',
    templateUrl: './cidade.component.html',
    styleUrls: ['./cidade.component.scss']
})
export class CidadeComponent implements OnInit {
    cidades: Cidade[] = [];
    estados = [
        { sigla: 'AC', nome: 'Acre' }, { sigla: 'AL', nome: 'Alagoas' }, { sigla: 'AP', nome: 'Amapá' },
        { sigla: 'AM', nome: 'Amazonas' }, { sigla: 'BA', nome: 'Bahia' }, { sigla: 'CE', nome: 'Ceará' },
        { sigla: 'DF', nome: 'Distrito Federal' }, { sigla: 'ES', nome: 'Espírito Santo' }, { sigla: 'GO', nome: 'Goiás' },
        { sigla: 'MA', nome: 'Maranhão' }, { sigla: 'MT', nome: 'Mato Grosso' }, { sigla: 'MS', nome: 'Mato Grosso do Sul' },
        { sigla: 'MG', nome: 'Minas Gerais' }, { sigla: 'PA', nome: 'Pará' }, { sigla: 'PB', nome: 'Paraíba' },
        { sigla: 'PR', nome: 'Paraná' }, { sigla: 'PE', nome: 'Pernambuco' }, { sigla: 'PI', nome: 'Piauí' },
        { sigla: 'RJ', nome: 'Rio de Janeiro' }, { sigla: 'RN', nome: 'Rio Grande do Norte' }, { sigla: 'RS', nome: 'Rio Grande do Sul' },
        { sigla: 'RO', nome: 'Rondônia' }, { sigla: 'RR', nome: 'Roraima' }, { sigla: 'SC', nome: 'Santa Catarina' },
        { sigla: 'SP', nome: 'São Paulo' }, { sigla: 'SE', nome: 'Sergipe' }, { sigla: 'TO', nome: 'Tocantins' }
    ];

    modalAberto = false;
    cidadeForm!: FormGroup;

    constructor(private cidadeService: CidadeService, private fb: FormBuilder) { }

    ngOnInit(): void {
        this.inicializarFormulario();
        this.listarCidades();
    }

    inicializarFormulario(): void {
        this.cidadeForm = this.fb.group({
            nome: ['', Validators.required],
            uf: ['', Validators.required]
        });
    }

    listarCidades(): void {
        this.cidadeService.getCidades().subscribe(
            (data) => this.cidades = data,
            (error) => console.error('Erro ao carregar cidades:', error)
        );
    }

    abrirCadastro(): void {
        this.modalAberto = true;
        this.cidadeForm.reset();
    }

    fecharCadastro(): void {
        this.modalAberto = false;
    }

    salvarCidade(): void {
        if (this.cidadeForm.invalid) return;

        const novaCidade: Cidade = { ...this.cidadeForm.value };

        delete novaCidade.id;
        // console.log("Enviando para API:", JSON.stringify(novaCidade, null, 2));
        this.cidadeService.createCidade(novaCidade).subscribe(() => {
            alert('Cidade cadastrada com sucesso!');
            this.fecharCadastro();
            this.listarCidades();
        }, error => {
            console.error('Erro ao cadastrar cidade:', error);
        });
    }

    deletarCidade(id: number): void {
        if (confirm('Tem certeza que deseja excluir esta cidade?')) {
            this.cidadeService.deleteCidade(id).subscribe(() => {
                alert('Cidade deletada com sucesso!');
                this.listarCidades();
            }, error => {
                console.error('Erro ao deletar cidade:', error);
            });
        }
    }

}
