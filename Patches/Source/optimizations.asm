;===================================================================================================
; Fast ROM fixes
;===================================================================================================
org $80CC43 : LDA.b #$80
org $80CC67 : LDA.b #$80
org $808E66 : PHK

; Fix interrupts to go to fast rom sooner
org $808F88
NMI:
	JML ++
	NOP
++	PHB
	PHK
	PLB
	REP #$30
	PHD
	PHA
	PHX
	PHY


org $808F50
IRQ:
	JML ++
	NOP
++	PHB
	PHK
	PLB
	REP #$30
	SEI
	PHD
	PHA
	PHX
	PHY

; vanilla uses PHP : PLP, which is not necessary for interrupts
org $808FE5 : RTI
org $808F86 : RTI
org $808F86 : RTI

;===================================================================================================
; Some VS COM fixes
;===================================================================================================
org $899C82
	LDA.w $13B3
	CMP.b #$01
	RTS

;===================================================================================================
; Random fixes
;===================================================================================================
org $8084C2
	LDA.b #$01
--	BIT.w $4212
	BNE --

;===================================================================================================

; these vanilla routines are overkill on the stack use

org $808323
WaitForNMI:
	PHP

	SEP #$20

	LDA.b #$01
	STA.b $DD

.wait
	LDA.b $DD
	BNE .wait

	PLP
	RTL

;===================================================================================================

org $808358
WaitForNMIWithFBlank:
	PHP

	SEP #$20

	LDA.b #$80
	TSB.b $6A
	STA.b $DD

.wait
	LDA.b $DD
	BNE .wait

	PLP
	RTL

;===================================================================================================

org $80836B
WaitForNMIWithoutFBlank:
	PHP

	SEP #$20

	LDA.b #$80
	TRB.b $6A
	STA.b $DD

.wait
	LDA.b $DD
	BNE .wait

	PLP
	RTL

;===================================================================================================
; Random fixes
;===================================================================================================
org $828250
	LDA.b #$20
	ORA.b $2B
	STA.w $0DF1,X
	LDA.b #$0B
	STA.w $0E11,X
	INC.w $0DEF
	RTS

org $828372
	LDA.b #$20
	ORA.b $2B
	STA.w $0E9E,X
	LDA.b #$0B
	STA.w $0E9E,X
	INC.w $0E9C
	RTS

;---------------------------------------------------------------------------------------------------

org $839EC0
	RTS

;---------------------------------------------------------------------------------------------------


;===================================================================================================
; JSR : RTS => JMP
;===================================================================================================
org $809BF6 : db $4C
org $809C2C : db $4C
org $809C60 : db $4C
org $80A1B2 : db $4C
org $80A1B6 : db $4C
org $80A1BF : db $4C
org $80A1E3 : db $4C
org $80A1E7 : db $4C
org $80A1F0 : db $4C
org $80A1F6 : db $4C
org $80A1FC : db $4C
org $80A24A : db $4C
org $80A24E : db $4C
org $80A25B : db $4C
org $80A280 : db $4C
org $80A284 : db $4C
org $80A291 : db $4C
org $80A733 : db $4C
org $80BA7D : db $4C
org $8197D3 : db $4C
org $81AF3C : db $4C
org $81B1E9 : db $4C
org $81B577 : db $4C
org $81BA49 : db $4C
org $81BD81 : db $4C
org $81BE5C : db $4C
org $81CBC1 : db $4C
org $81CEB4 : db $4C
org $82B555 : db $4C
org $82B64C : db $4C
org $82B700 : db $4C
org $839D4F : db $4C
org $839D58 : db $4C
org $839D72 : db $4C
org $839D85 : db $4C
org $839D95 : db $4C
org $839DA3 : db $4C
org $839DBA : db $4C
org $83A32B : db $4C
org $83A346 : db $4C
org $83A361 : db $4C
org $83A37C : db $4C
org $83AC8E : db $4C
org $83ACB0 : db $4C
org $83C3E8 : db $4C
org $83C406 : db $4C
org $85F0DE : db $4C
org $85F149 : db $4C
org $85F194 : db $4C
org $85F1BB : db $4C
org $85F1F4 : db $4C
org $85F24E : db $4C
org $8793DA : db $4C
org $879593 : db $4C
org $879610 : db $4C
org $87970D : db $4C
org $8797C4 : db $4C
org $888735 : db $4C
org $88875A : db $4C
org $88876B : db $4C
org $8888F0 : db $4C
org $888915 : db $4C
org $888926 : db $4C
org $888FFD : db $4C
org $889096 : db $4C
org $88A22F : db $4C
org $88A31E : db $4C
org $88A3A9 : db $4C
org $88A69F : db $4C
org $88A6BF : db $4C
org $88A7C4 : db $4C
org $88A85F : db $4C
org $88AED6 : db $4C
org $88B3CA : db $4C
org $88BA68 : db $4C
org $88BA71 : db $4C
org $88C070 : db $4C
org $88C111 : db $4C
org $88C1C7 : db $4C
org $88C335 : db $4C
org $88CD37 : db $4C
org $88CF94 : db $4C
org $88D198 : db $4C
org $88D1FA : db $4C
org $88D42A : db $4C
org $88D42E : db $4C
org $88D4C0 : db $4C
org $88D537 : db $4C
org $88D645 : db $4C
org $88D6A6 : db $4C
org $88D6DB : db $4C
org $88D713 : db $4C
org $88D72B : db $4C
org $88D7D0 : db $4C
org $88E03D : db $4C
org $88E1BC : db $4C
org $88E1E5 : db $4C
org $88E343 : db $4C
org $88E35F : db $4C
org $88E37B : db $4C
org $88E471 : db $4C
org $88E65F : db $4C
org $88E73F : db $4C
org $88EB83 : db $4C
org $88EC59 : db $4C
org $88EF77 : db $4C
org $88EF8D : db $4C
org $88EFA3 : db $4C
org $88F025 : db $4C
org $88F0DE : db $4C
org $899734 : db $4C
org $89973F : db $4C
org $899743 : db $4C
org $89974E : db $4C
org $899752 : db $4C
org $899B72 : db $4C
org $899EB7 : db $4C
org $8B8679 : db $4C

; JSR (abs,X) : RTS => JMP (abs,X)
org $878BA4 : db $7C
org $878CC0 : db $7C
org $878E47 : db $7C
org $879136 : db $7C
org $8793F3 : db $7C
org $8795A6 : db $7C
org $87971C : db $7C
org $8797D1 : db $7C
org $889D77 : db $7C
org $88D68F : db $7C
org $88D69C : db $7C
org $88E027 : db $7C
org $88E065 : db $7C
org $88E394 : db $7C
org $88E582 : db $7C
org $88E6F4 : db $7C
org $88E7BE : db $7C
org $88E886 : db $7C
org $88E8D2 : db $7C
org $88EB8C : db $7C
org $88EFB6 : db $7C
org $88F06F : db $7C
org $88F122 : db $7C
org $88F16E : db $7C
org $88F1E4 : db $7C
org $88F33F : db $7C
org $898742 : db $7C
org $89884A : db $7C
org $89920C : db $7C
org $89A0DC : db $7C
org $89A131 : db $7C
org $89A13E : db $7C
org $89A1E9 : db $7C
org $89A248 : db $7C
org $89A2F3 : db $7C
org $89A36C : db $7C
org $89A4E0 : db $7C
org $89A53F : db $7C
org $89A5DA : db $7C
org $89A639 : db $7C
org $89A722 : db $7C
org $89A781 : db $7C
org $89A89C : db $7C
org $89AAB5 : db $7C
org $89AB14 : db $7C
org $89ABBF : db $7C
org $89AC2C : db $7C
org $89AD97 : db $7C
org $89ADF6 : db $7C
org $89AEC7 : db $7C
org $89AF26 : db $7C
org $89B005 : db $7C
org $89B070 : db $7C
org $89B148 : db $7C
org $89B1BF : db $7C
org $89B2B8 : db $7C
org $89B317 : db $7C
org $89B3FC : db $7C
org $89B45B : db $7C
org $89B504 : db $7C
org $89B56F : db $7C
org $89B637 : db $7C
org $89B696 : db $7C
org $89B76D : db $7C
org $89B7CC : db $7C
org $89B871 : db $7C
org $89B8D2 : db $7C
org $89B967 : db $7C
org $89B9CE : db $7C
org $89BA51 : db $7C
org $88CCAE : db $7C
org $88E01A : db $7C
org $898D99 : db $7C

; JSL : RTL => JML
org $82B35A : db $5C

